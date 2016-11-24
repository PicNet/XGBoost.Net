using System;
using XGBoost;
using Microsoft.VisualBasic.FileIO;

namespace Demo
{
  class Program
  {
    static int trainCols = 126;
    static int trainRows = 6513;
    static int testCols = 126;
    static int testRows = 1611;

    static void Main(string[] args)
    {
      
      float[][] dataTrain = new float[2][];
      dataTrain[0] = new float[1];
      dataTrain[1] = new float[1];
      dataTrain[0][0] = 0.1F;
      dataTrain[1][0] = 0.9F;
      float[] labelsTrain = { 0.1F, 0.9F };
      float[][] dataTest = dataTrain;
      
      /*
      float[][] dataTrain = GetDataTrain();
      float[] labelsTrain = GetLabelsTrain();
      float[][] dataTest = GetDataTest();
      */

      XGBRegressor xgbr = new XGBRegressor();
      xgbr.Fit(dataTrain, labelsTrain);
      float[] preds = xgbr.Predict(dataTest);
      PrintPreds(preds);
    }

    static float[][] GetDataTrain()
    {
      using (TextFieldParser parser = new TextFieldParser(@"C:\dev\tests\testXgboost\agaricus.train.csv"))
      {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        float[][] dataTrain = new float[trainRows][];
        int row = 0;

        while (!parser.EndOfData)
        {
          dataTrain[row] = new float[trainCols];
          string[] fields = parser.ReadFields();

          // skip label column in csv file
          for (int col = 1; col < fields.Length; col++)
          {
            dataTrain[row][col - 1] = float.Parse(fields[col]);
          }
          row += 1;
        }

        return dataTrain;
      }
    }

    static float[] GetLabelsTrain()
    {
      using (TextFieldParser parser = new TextFieldParser(@"C:\dev\tests\testXgboost\agaricus.train.csv"))
      {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        float[] labelsTrain = new float[trainRows];
        int row = 0;

        while (!parser.EndOfData)
        {
          string[] fields = parser.ReadFields();
          labelsTrain[row] = float.Parse(fields[0]);
          row += 1;
        }

        return labelsTrain;
      }
    }

    static float[][] GetDataTest()
    {
      using (TextFieldParser parser = new TextFieldParser(@"C:\dev\tests\testXgboost\agaricus.test.csv"))
      {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        float[][] dataTest = new float[testRows][];
        int row = 0;

        while (!parser.EndOfData)
        {
          dataTest[row] = new float[testCols];
          string[] fields = parser.ReadFields();

          // skip label column in csv file
          for (int col = 1; col < fields.Length; col++)
          {
            dataTest[row][col - 1] = float.Parse(fields[col]);
          }
          row += 1;
        }

        return dataTest;
      }
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
