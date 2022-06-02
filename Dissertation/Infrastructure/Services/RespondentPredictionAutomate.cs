using Accord;
using System.Data;
using Accord.Math;
using Accord.MachineLearning.DecisionTrees;
using Dissertation.Persistence.Entities;
using Dissertation.Common.Services;
using Microsoft.EntityFrameworkCore;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Common.Services.AutomationLogic;

namespace Dissertation.Infrastructure.Services;

public class RespondentPredictionAutomate : BaseDecisionTreeLogic, IRespondentAutomationLogic
{
    private readonly IApplicationDbContext _context;
    private IIncident _incident;

    public RespondentPredictionAutomate(IApplicationDbContext context,
        string tableName = nameof(RespondentPredictionAutomate))
        : base(tableName) => (_context) = (context);

    public override void AutomateStartProcess() { }

    protected override void InitDefault()
    {
        _data.Columns.Add("Id", "Coincidence", "SpecialistField", "Busy", "Relax", "Normal", "OverThreeFourth", "OverOneFifth", "Workload");

        _data.Rows.Add("D1", true, "null", false, true, false, false, false, Workload.Neutral);
        _data.Rows.Add("D2", true, "null", false, true, false, false, false, Workload.Neutral);
        _data.Rows.Add("D3", false, false, false, true, false, false, false, Workload.Neutral);
        _data.Rows.Add("D4", "null", false, false, true, false, false, false, Workload.Neutral);

        _data.Rows.Add("D5", true, true, false, false, true, false, false, Workload.Low);
        _data.Rows.Add("D6", true, "null", false, true, false, false, false, Workload.Low);
        _data.Rows.Add("D7", false, false, false, false, true, false, false, Workload.Low);
        _data.Rows.Add("D8", "null", true, false, true, false, false, false, Workload.Low);

        _data.Rows.Add("D9", true, "null", false, false, true, false, false, Workload.Normal);
        _data.Rows.Add("D10", false, "null", false, false, true, false, false, Workload.Normal);
        _data.Rows.Add("D11", true, true, false, false, true, false, false, Workload.Normal);
        _data.Rows.Add("D12", false, false, false, false, true, false, false, Workload.Normal);

        _data.Rows.Add("D13", true, true, true, false, false, false, true, Workload.High);
        _data.Rows.Add("D14", false, true, true, false, true, false, true, Workload.High);
        _data.Rows.Add("D15", true, true, false, false, false, false, true, Workload.High);
        _data.Rows.Add("D16", false, true, false, false, "null", false, true, Workload.High);

        _data.Rows.Add("D17", true, false, true, false, false, true, "null", Workload.Exceed);
        _data.Rows.Add("D18", false, "null", true, false, "null", true, "null", Workload.Exceed);
        _data.Rows.Add("D19", true, false, true, false, false, true, "null", Workload.Exceed);
        _data.Rows.Add("D20", false, "null", true, false, "null", true, "null", Workload.Exceed);

        _data.Rows.Add("D21", "null", false, true, false, false, true, "null", Workload.Critical);
        _data.Rows.Add("D22", "null", "null", true, false, false, true, "null", Workload.Critical);
        _data.Rows.Add("D23", "null", false, true, false, false, true, "null", Workload.Critical);
        _data.Rows.Add("D24", "null", "null", true, false, false, true, "null", Workload.Critical);

        if (_codification == null)
        {
            ExecuteCodification();
        }

        DataTable symbols = _codification!.Apply(_data);

        outputs = symbols.ToArray<int>("Workload");
        inputs = symbols.ToJagged<int>(
            "Coincidence", "SpecialistField",
            "Busy", "Relax", "Normal",
            "OverThreeFourth", "OverOneFifth"
            );

        var decisionVariable = new DecisionVariable[]
        {
            new DecisionVariable("Coincidence", _codification["Coincidence"].NumberOfSymbols),
            new DecisionVariable("SpecialistField", _codification["SpecialistField"].NumberOfSymbols),
            new DecisionVariable("Busy", _codification["Busy"].NumberOfSymbols),
            new DecisionVariable("Relax",  _codification["Relax"].NumberOfSymbols),
            new DecisionVariable("Normal",  _codification["Normal"].NumberOfSymbols),
            new DecisionVariable("OverThreeFourth", _codification["OverThreeFourth"].NumberOfSymbols),
            new DecisionVariable("OverOneFifth", _codification["OverOneFifth"].NumberOfSymbols),
        };

        StartLearn(decisionVariable);
    }

    public PriorityQueue<(Guid, StaffType), Workload> GetPriorityQueueWorkloadStatistic(IIncident incident)
    {
        _incident = incident;

        var staffStatistics = _context.StaffStatistics.Include(x => x.Staff).Include(x => x.JobSamples)
            .Where(x => x.StatisticsType == incident.Type &&
                (x.Staff.Type == StaffType.CyberSecuritySpecialist || x.Staff.Type == StaffType.Analyst))
            .ToList();

        var totalCountJob = _context.StaffStatistics.Include(x => x.JobSamples)
            .Where(x => x.StatisticsType == incident.Type)
            .Select(x => x.JobSamples.Count)
            .ToArray()
            .Sum();

        var staffCount = _context.Staffs
            .Where(x => x.Type == StaffType.CyberSecuritySpecialist || x.Type == StaffType.Analyst)
            .Count();

        Dictionary<Guid, double> currentJobCountPercents = new();
        PriorityQueue<(Guid, StaffType), Workload> priorityQueue = new();
        List<((Guid, StaffType), Workload)> resultList = new();

        foreach (var statistic in staffStatistics)
        {
            var currentTaskCount = statistic.JobSamples.Where(x => x.Stage != Stage.Completed).Count();
            currentJobCountPercents.Add(statistic.Id, GetPercent(totalCountJob, currentTaskCount));
        }

        foreach (var statistic in staffStatistics)
        {
            var completedTask = statistic.JobSamples.Where(x => x.Stage == Stage.Completed).Count();
            var currentTaskCount = statistic.JobSamples.Where(x => x.Stage != Stage.Completed).Count();


            int[] query = _codification.Transform(new[,]
            {
                { "Coincidence",  GetCoincidence(statistic)},
                { "SpecialistField", GetSpecialistFieldValue(totalCountJob, staffCount, completedTask, statistic) },
                { "Busy", IsBusy(currentJobCountPercents, statistic.Id).ToString() },
                { "Relax", IsRelax(currentJobCountPercents[statistic.Id]).ToString() },
                { "Normal",IsNormal(currentJobCountPercents[statistic.Id]).ToString() },
                { "OverOneFifth", GetOverOneFifth(currentJobCountPercents[statistic.Id]).ToString() },
                { "OverThreeFourth", IsOverThreeFourth(currentJobCountPercents[statistic.Id]).ToString() },
            });

            string workloadStr = string.Empty;

            try
            {
                workloadStr = GetPredictedAnswer(query);
            }
            catch (Exception ex)
            {
                workloadStr = "Normal";
            }


            resultList.Add(((statistic.Id, statistic.Staff.Type),
                Enum.Parse<Workload>(workloadStr)));
        }

        priorityQueue = new(resultList);

        return priorityQueue;
    }

    private string GetCoincidence(StaffStatistic staffStatistic)
        => (staffStatistic.StatisticsType == _incident.Type).ToString();

    private string GetSpecialistFieldValue(int totalCountJob, int staffCount, int completedTask, StaffStatistic staffStatistic)
        => totalCountJob <= 0 || totalCountJob <= staffCount || !IsCoincidence(staffStatistic)
            ? "null"
            : ((totalCountJob / staffCount) > completedTask).ToString();

    private bool IsCoincidence(StaffStatistic staffStatistic)
        => (staffStatistic.StatisticsType == _incident.Type);

    private bool IsBusy(Dictionary<Guid, double> currentJobCountPercents, Guid Id)
    {
        var percent = currentJobCountPercents[Id];

        if (currentJobCountPercents.Count < 1 || percent < 1)
        {
            return false;
        }

        var arr = currentJobCountPercents.Values.ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == percent)
            {
                if ((arr.Length / 2) >= (i + 1))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private double GetPercent(double totalCountJob, double currentTaskCount)
        => currentTaskCount < 1 || totalCountJob < 1
            ? 0
            : Math.Round((double)((currentTaskCount / totalCountJob) * 100), 2);

    private string GetOverOneFifth(double percent)
        => IsOverThreeFourth(percent)
            ? "null"
            : IsOverOneFifth(percent).ToString();

    private bool IsRelax(double percent)
    => (percent <= 4.99);

    private bool IsNormal(double percent)
        => (percent >= 5.00 && percent <= 20.00);

    private bool IsOverOneFifth(double percent)
        => (percent >= 20.00);

    private bool IsOverThreeFourth(double percent)
        => (percent >= 75.00);

}

/// <summary>
/// Выдавать ответ на основе статистики загруженность Workload (Перезаписывать)
/// 
/// 1)Совпадает инцидент с типом статистики? 1/0
/// 
/// 2)Количество инцендентов по типу / количество респондентов > больше завершенных по типу? 1/0
/// 
/// 3)Процент от общих задач с текущих 100% от СurrentTaskCount больше к загруженным или свободным? 1/0
/// 0.05 0.05 0.05 0.1 0.15 0.15 0.2 0.25
/// 
/// 4)Процент от общих задач с текущих 100% от СurrentTaskCount меньше 5% 1/0
/// 
/// 5)Процент от общих задач с текущих 100% от СurrentTaskCount больше 5% и меньше 20% 1/0
/// 
/// 6)Процент от общих задач с текущих 100% от СurrentTaskCount больше 75% 1/0
/// 
/// 7)Процент от общих задач с текущих 100% от СurrentTaskCount больше 20% 1/0
/// ----------------------------------------------------
/// Neutral
/// 1) Да null null Да Нет Нет Нет
/// 2) Нет null null Да Нет Нет Нет
/// 
/// Low
/// 1) Да Да Нет Нет Да Нет Нет
/// 2) Нет Да Нет Нет Да Нет Нет
/// 
/// Normal
/// 1) Да null Нет Нет Нет Нет Нет
/// 2) Нет null Нет Нет null Нет Нет
/// 
/// High
/// 1) Да Да Нет Нет Нет Нет Да
/// 2) Нет Да Нет Нет null Нет Да
/// 
/// Exceed
/// 1) Да Нет Нет Нет Нет Нет Да
/// 2) Нет Нет Нет Нет null Нет Да
/// 
/// Critical
/// 1) null null Да Нет Нет Да null
/// 2) null null null Нет Нет Да null
/// </summary>
