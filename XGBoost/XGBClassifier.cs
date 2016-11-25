using System;
using System.Collections.Generic;
using System.Linq;

namespace XGBoost
{
  public class XGBClassifier
  {
    private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();
    private Booster booster;

    public XGBClassifier(int maxDepth = 3, float learningRate = 0.1F, int nEstimators = 100,
                        bool silent = true, string objective = "binary:logistic",
                        int nThread = -1, float gamma = 0, int minChildWeight = 1,
                        int maxDeltaStep = 0, float subsample = 1, float colSampleByTree = 1,
                        float colSampleByLevel = 1, float regAlpha = 0, float regLambda = 1,
                        float scalePosWeight = 1, float baseScore = 0.5F, int seed = 0,
                        float missing = float.NaN)
    {
      parameters["max_depth"] = maxDepth;
      parameters["learning_rate"] = learningRate;
      parameters["n_estimators"] = nEstimators;
      parameters["silent"] = silent;
      parameters["objective"] = objective;

      parameters["nthread"] = nThread;
      parameters["gamma"] = gamma;
      parameters["min_child_weight"] = minChildWeight;
      parameters["max_delta_step"] = maxDeltaStep;
      parameters["subsample"] = subsample;
      parameters["colsample_bytree"] = colSampleByTree;
      parameters["colsample_bylevel"] = colSampleByLevel;
      parameters["reg_alpha"] = regAlpha;
      parameters["reg_lambda"] = regLambda;
      parameters["scale_pos_weight"] = scalePosWeight;

      parameters["base_score"] = baseScore;
      parameters["seed"] = seed;
      parameters["missing"] = missing;
      parameters["_Booster"] = null;
    }

    /*
     * TODO: Most of these paramaters probably don't work now since their behaviour needs to
     *     public void Fit(float[][] data, float[] labels, float[][] evalSet = null,
                    string evalMetric = null, int? earlyStoppingRounds = null,
                    bool verbose = true)
     * be implemented. Only the essential parameters are actually implemented
     */
    public void Fit(float[][] data, float[] labels)
    {
      var train = new DMatrix(data, labels);
      booster = Train(parameters, train, ((int)parameters["n_estimators"]));
    }

    /*
     * TODO: Most of these paramaters probably don't work now since their behaviour needs to
     *     public float[] Predict(float[][] data, bool outputMargin = false,
                           int nTreeLimit = 0)
     * be implemented. Only the essential parameters are actually implemented
     */
    public float[] Predict(float[][] data)
    {
      var test = new DMatrix(data);
      var preds = booster.Predict(test);
      return preds.Select(v => v > 0.5f ? 1f : 0f).ToArray();
    }

    /*
     * float[][] data, bool outputMargin = false, int nTreeLimit = 0
     */
    public float[][] PredictProba(float[][] data)
    {
      var dTest = new DMatrix(data);
      var preds = booster.Predict(dTest);
      return preds.Select(v => v > 0.5f ? new [] {v, 1 - v} : new[] { 1 - v, v }).ToArray();
    }
    
    private Booster Train(IDictionary<string, object> args, DMatrix dTrain,
                         int numBoostRound = 10, Tuple<DMatrix, string>[] evals = null,
                         Func<string, float> obj = null, Func<float, float> fEval = null,
                         bool maximize = false, int? earlyStoppingRounds = null,
                         Object evalsResult = null,
                         bool verboseEval = true, Object learningRates = null,
                         string xgbModel = null, Object[] callbacks = null)
    {
      var bst = new Booster(args, dTrain);
      for (int i = 0; i < numBoostRound; i++) { bst.Update(dTrain, i); }
      return bst;
    }
  }
}
