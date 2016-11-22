using System;
using XGBoost;

namespace Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      float[][] dataTrain = new float[2][];
      dataTrain[0] = new float[1];
      dataTrain[1] = new float[1];
      dataTrain[0][0] = 0;
      dataTrain[1][0] = 1;
      float[] labels = { 0, 1 };

      XGBRegressor xgbr = new XGBRegressor();
      xgbr.Fit(dataTrain, labels);
      float[][] dataTest = dataTrain;
      float[] preds = xgbr.Predict(dataTest);
      for (int i = 0; i < preds.Length; i++)
      {
        Console.Write(preds[i].ToString() + " ");
      }
      Console.ReadKey();
    }
  }
}
