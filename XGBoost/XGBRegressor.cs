namespace XGBoost
{
  class XGBRegressor
  {
    private int _boostRounds = 10;
    private Booster _booster;

    public XGBRegressor()
    { 
    }

    public void Fit(float[][] data, float[] labels)
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

    public float[] Predict(float[][] data)
    {
      DMatrix dTest = new DMatrix(data);
      float[] preds = _booster.Predict(dTest);
      return preds;
    }
  }
}
