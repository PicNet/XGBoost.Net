using System;
using System.Collections.Generic;

namespace XGBoost
{
  public class XGBRegressor
  {
    public IDictionary<string, object> parameters = new Dictionary<string, object>();

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
      parameters["maxDepth"] = maxDepth;
      parameters["learningRate"] = learningRate;
      parameters["nEstimators"] = nEstimators;
      parameters["silent"] = silent;
      parameters["objective"] = objective;

      parameters["nThread"] = nThread;
      parameters["gamma"] = gamma;
      parameters["minChildWeight"] = minChildWeight;
      parameters["maxDeltaStep"] = maxDeltaStep;
      parameters["subsample"] = subsample;
      parameters["colSampleByTree"] = colSampleByTree;
      parameters["colSampleByLevel"] = colSampleByLevel;
      parameters["regAlpha"] = regAlpha;
      parameters["regLambda"] = regLambda;
      parameters["scalePosWeight"] = scalePosWeight;

      parameters["baseScore"] = baseScore;
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
      parameters["_Booster"] = Train(parameters, dTrain);
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
                         string xgbModel = null, Object callbacks[] = null)
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
      float[] preds = ((Booster)parameters["_Booster"]).Predict(dTest);
      return preds;
    }
  }
}
