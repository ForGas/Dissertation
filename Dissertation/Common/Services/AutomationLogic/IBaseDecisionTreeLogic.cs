using Accord.MachineLearning.DecisionTrees;
using System.Data;

namespace Dissertation.Common.Services.AutomationLogic;

public interface IBaseDecisionTreeLogic : IAutomationLogic
{
    DataTable Data { get; }
    void StartLearn(DecisionVariable[] decisionVariables);
    void AutomateStartProcess();
    void ExecuteCodification();
    string GetPredictedAnswer(int[] query);
    int[] GetCodificationQuery(string[,] query);
}
