using System;
using XGBoost;

namespace Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      float[][] dataTrain = GetDataTrain();
      float[] labels = GetLabels();
      float[][] dataTest = GetDataTest();

      XGBRegressor xgbr = new XGBRegressor();
      xgbr.Fit(dataTrain, labels);
      float[] preds = xgbr.Predict(dataTest);
      PrintPreds(preds);
    }

    static float[][] GetDataTrain()
    {
      float[][] dataTrain = new float[2][];
      dataTrain[0] = new float[1];
      dataTrain[1] = new float[1];
      dataTrain[0][0] = 0;
      dataTrain[1][0] = 1;
      return dataTrain;
    }

    static float[] GetLabels()
    {
      float[] labels = { 0, 1 };
      return labels;
    }

    static float[][] GetDataTest()
    {
      float[][] dataTrain = new float[2][];
      dataTrain[0] = new float[1];
      dataTrain[1] = new float[1];
      dataTrain[0][0] = 0;
      dataTrain[1][0] = 1;
      return dataTrain;
    }

    static void PrintPreds(float[] preds)
    {
      for (int i = 0; i < preds.Length; i++)
      {
        Console.Write(preds[i].ToString() + " ");
      }
      Console.ReadKey();
    }
  }
}
