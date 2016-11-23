namespace XGBoost
{
  public class XGBRegressor
  {
    private int _boostRounds = 10;
    private Booster _booster;

    private float _learningRate;

    public XGBRegressor(int maxDepth = 3, float learningRate = 0.1F, int nEstimators = 100,
                        bool silent = true, string objective = "reg:linear",
                        int nThread = -1, float gamma = 0, int minChildWeight = 1,
                        int maxDeltaStep = 0, float subsample = 1, float colSampleByTree = 1,
                        float colSampleByLevel = 1, float regAlpha = 0, float regLambda = 1,
                        float scalePosWeight = 1, float baseScore = 0.5F, int seed = 0,
                        float missing = float.NaN)
    {
      _learningRate = learningRate;
    }

    public void Fit(float[][] data, float[] labels, float[][] evalSet = null,
                    string evalMetric = null, int? earlyStoppingRounds = null,
                    bool verbose = true)
    {
      DMatrix dTrain = new DMatrix(data, labels);
      _booster = Train(dTrain);
    }

    public Booster Train(DMatrix dTrain)
    {
      Booster booster = new Booster(dTrain);
      for (int i = 0; i < _boostRounds; i++)
      {
        booster.Update(dTrain, i);
      }
      return booster;
    }

    public float[] Predict(float[][] data, bool outputMargin = false,
                           int nTreeLimit = 0)
    {
      DMatrix dTest = new DMatrix(data);
      float[] preds = _booster.Predict(dTest);
      return preds;
    }
  }
}
