using Accord;
using System.Data;
using Accord.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Filters;
using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Base;
using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Infrastructure.Services;

public interface IAutomationLogic
{
}

// TODO : доделать ограничие через Repository
public interface IBaseDecisionTreeLogic : IAutomationLogic
{
    DataTable Data { get; }
    IApplicationDbContext SetContext { set; }
    void StartLearn();
    void AutomateStartProcess();
    void ExecuteCodification();
    string GetPredictedAnswer(int[] query);
    int[] GetCodificationQuery(string[,] query);
}

public interface IRespondentAutomationLogic : IBaseDecisionTreeLogic
{

}

// TODO : доделать ограничение через Repository
public abstract class BaseDecisionTreeLogic<T>
    : IBaseDecisionTreeLogic, IDisposable
    where T : BaseIdentity
{
    private int _predictedValue;
    private double _errorValue;
    private IApplicationDbContext _context;
    private DecisionTree _tree;
    private bool _disposedValue;
    private readonly ID3Learning _decisionTreeLearningAlgorithm;

    protected Codification _codification;
    protected DataTable _data;
    protected int[][] inputs;
    protected int[] outputs;

    public BaseDecisionTreeLogic(string tableName)
    {
        (_data, _predictedValue, _decisionTreeLearningAlgorithm, _errorValue)
            = (new DataTable(tableName), default, new ID3Learning(), default);

        InitDefault();
    }

    public DataTable Data => _data;

    public int GetPredictedValue => _predictedValue;

    public IApplicationDbContext SetContext { set => _context = value; }

    public void ExecuteCodification() => _codification = new(_data);

    public int[] GetCodificationQuery(string[,] query) => _codification.Transform(query);

    protected abstract void InitDefault();

    protected abstract void SetData(Span<T> collections);

    public abstract void AutomateStartProcess();

    public void StartLearn()
    {
        _decisionTreeLearningAlgorithm.Attributes = DecisionVariable.FromCodebook(_codification);
        _tree = _decisionTreeLearningAlgorithm.Learn(inputs, outputs);
        _errorValue = new ZeroOneLoss(outputs).Loss(_tree.Decide(inputs));
    }

    public double GetError() => _errorValue;

    public string GetPredictedAnswer(int[] query)
    {
        _predictedValue = _tree.Decide(query);
        var predictedRowName = _data.Rows[_data.Rows.Count - 1].ToString();
        return _codification.Revert(predictedRowName, _predictedValue);
    }

    public void Dispose()
    {
        if (_data != null)
        {
            _data.Clear();
            _data.Dispose();
        }
    }
}

public class RespondentPredictionAutomate
    : BaseDecisionTreeLogic<RespondentStatistics>, IRespondentAutomationLogic
{
    public RespondentPredictionAutomate(string tableName = nameof(RespondentPredictionAutomate))
        : base(tableName) { }

    public override void AutomateStartProcess()
    {
        //InitDefault()
        //SetData() -> and symbols and logic
        //ExecuteCodification()
        // public PriorityQueue<string, int> PriorityQueue { get; set; }
        //StartLearn()
        //GetCodificationQuery()
        //GetPredictedAnswer()
    }

    protected override void InitDefault()
    {
        _data.Columns.Add(
            "Id", "Coincidence", "SpecialistField",
            "Busy", "Relax", "Normal",
            "OverThird", "OverFifth", "Workload"
            );

        _data.Rows.Add("D1", 1, null, null, 1, 0, 0, 0, Workload.Neutral);
        _data.Rows.Add("D2", 0, null, null, 1, 0, 0, 0, Workload.Neutral);

        _data.Rows.Add("D3", 1, 1, 0, 0, 1, 0, 0, Workload.Low);
        _data.Rows.Add("D4", 0, 1, 0, 0, 1, 0, 0, Workload.Low);

        _data.Rows.Add("D5", 1, null, 0, 0, 0, 0, 0, Workload.Normal);
        _data.Rows.Add("D6", 0, null, 0, 0, null, 0, 0, Workload.Normal);

        _data.Rows.Add("D7", 1, 1, 0, 0, 0, 0, 1, Workload.High);
        _data.Rows.Add("D8", 0, 1, 0, 0, null, 0, 1, Workload.High);

        _data.Rows.Add("D9", 1, 0, 0, 0, 0, 0, 1, Workload.Exceed);
        _data.Rows.Add("D10", 0, 0, 0, 0, null, 0, 1, Workload.Exceed);

        _data.Rows.Add("D11", null, null, 1, 0, 0, 1, null, Workload.Critical);
        _data.Rows.Add("D12", null, null, null, 0, 0, 1, null, Workload.Critical);

        if (_codification == null)
        {
            ExecuteCodification();
        }

        DataTable symbols = _codification!.Apply(_data);

        outputs = symbols.ToArray<int>("Workload");
        inputs = symbols.ToJagged<int>(
            "Coincidence", "SpecialistField",
            "Busy", "Relax", "Normal",
            "OverThird", "OverFifth"
            );
    }

    protected override void SetData(Span<RespondentStatistics> collections)
    {
        // Использовать Span
        //DecisionVariable.FromCodebook(_codification, )
    }

    public int Logic()
    {
        Span<RespondentStatistics> statistics = new();

        int allCount = 1000;

        int[] result = new int[7];

        if (statistics[0].StatisticsType == StatisticsType.File)
            result[0] = 1;
        else
            result[0] = 0;

        var two = allCount / statistics.Length > statistics[0].CompletedTaskCount;

        var three = ((statistics[0].СurrentTaskCount / allCount) * 100);

        if (three < 5)
        {

        }

        if (three <= 5)
        {

        }

        if (three >= 5 && three <= 20)
        {

        }

        if (three >= 20)
        {

        }

        if (three >= 75)
        {

        }

        return 1;
    }
}

public class RespondentStatistics : AuditableEntity
{
    public Guid RespondentId { get; set; }
    public int СurrentTaskCount { get; set; }
    public int CompletedTaskCount { get; set; }
    public bool IsRelevance { get; set; }
    public StatisticsType StatisticsType { get; set; }
    public Workload Workload { get; set; }
}

/// <summary>
/// Выдавать ответ на основе статистики загруженность Workload (Перезаписывать)
/// 
/// 1)Совпадает инцидент с типом статистики? 1/0
/// 
/// 2)Количество инцендентов по типу / количество респондентов > больше завершенных по типу? 1/0
/// 
/// 3)Процент от общих задач с текущих 100% от СurrentTaskCount больше к загруженным или свободным? 1/0
/// 0.5 0.5 0.5 0.1 0.15 0.15 0.2 .25
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
