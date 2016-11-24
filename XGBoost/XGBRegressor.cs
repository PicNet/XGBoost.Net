using System;
using System.Collections.Generic;

namespace XGBoost
{
  public class XGBRegressor
  {
    public IDictionary<string, object> parameters = new Dictionary<string, object>();
    public Booster booster;

    /*
     * TODO: Most of these paramaters probably don't work now since their behaviour needs to
     * be implemented. Only the essential parameters are actually implemented
     */
    public XGBRegressor(int maxDepth = 3, float learningRate = 0.1F, int nEstimators = 100,
                        bool silent = true, string objective = "reg:linear",
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
     * be implemented. Only the essential parameters are actually implemented
     */
    public void Fit(float[][] data, float[] labels, float[][] evalSet = null,
                    string evalMetric = null, int? earlyStoppingRounds = null,
                    bool verbose = true)
    {
      DMatrix dTrain = new DMatrix(data, labels);
      booster = Train(parameters, dTrain, ((int)parameters["n_estimators"]));
    }

    /*
     * TODO: Most of these paramaters probably don't work now since their behaviour needs to
     * be implemented. Only the essential parameters are actually implemented
     */
    public Booster Train(IDictionary<string, object> parameters, DMatrix dTrain,
                         int numBoostRound = 10, Tuple<DMatrix, string>[] evals = null,
                         Func<string, float> obj = null, Func<float, float> fEval = null,
                         bool maximize = false, int? earlyStoppingRounds = null,
                         Object evalsResult = null,
                         bool verboseEval = true, Object learningRates = null,
                         string xgbModel = null, Object[] callbacks = null)
    {
      Booster booster = new Booster(parameters, dTrain);
      for (int i = 0; i < numBoostRound; i++)
      {
        booster.Update(dTrain, i);
      }
      return booster;
    }

    /*
     * TODO: Most of these paramaters probably don't work now since their behaviour needs to
     * be implemented. Only the essential parameters are actually implemented
     */
    public float[] Predict(float[][] data, bool outputMargin = false,
                           int nTreeLimit = 0)
    {
      DMatrix dTest = new DMatrix(data);
      float[] preds = booster.Predict(dTest);
      return preds;
    }
  }
}
