using System.Collections;
using System.Collections.Generic;

namespace XGBoost
{
  public class XGBRegressor
  {
    public IDictionary<string, object> parameters = new Dictionary<string, object>();

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

    public void Fit(float[][] data, float[] labels, float[][] evalSet = null,
                    string evalMetric = null, int? earlyStoppingRounds = null,
                    bool verbose = true)
    {
      DMatrix dTrain = new DMatrix(data, labels);
      parameters["_Booster"] = Train(dTrain);
    }

    public Booster Train(DMatrix dTrain)
    {
      Booster booster = new Booster(dTrain);
      int boostRounds = 10;
      for (int i = 0; i < boostRounds; i++)
      {
        booster.Update(dTrain, i);
      }
      return booster;
    }

    public float[] Predict(float[][] data, bool outputMargin = false,
                           int nTreeLimit = 0)
    {
      DMatrix dTest = new DMatrix(data);
      float[] preds = ((Booster)parameters["_Booster"]).Predict(dTest);
      return preds;
    }
  }
}
