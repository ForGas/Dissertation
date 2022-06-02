using System.Data;
using Accord.Statistics.Filters;
using Accord.Math.Optimization.Losses;
using Dissertation.Persistence.Entities.Base;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;

namespace Dissertation.Common.Services.AutomationLogic;

public abstract class BaseDecisionTreeLogic : IBaseDecisionTreeLogic
{
    private int _predictedValue;
    private double _errorValue;
    private DecisionTree _tree;
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

    public void ExecuteCodification() => _codification = new(_data);

    public int[] GetCodificationQuery(string[,] query) => _codification.Transform(query);

    protected abstract void InitDefault();

    public abstract void AutomateStartProcess();

    public void StartLearn(DecisionVariable[] decisionVariables)
    {
        _decisionTreeLearningAlgorithm.Attributes = decisionVariables;
        _tree = _decisionTreeLearningAlgorithm.Learn(inputs, outputs);
        _errorValue = new ZeroOneLoss(outputs).Loss(_tree.Decide(inputs));
    }

    public double GetError() => _errorValue;

    public string GetPredictedAnswer(int[] query)
    {
        _predictedValue = _tree.Decide(query);
        var predictedRowName = _data.Columns[_data.Columns.Count - 1].ColumnName;
        return _codification.Revert(predictedRowName, _predictedValue);
    }
}
