using System;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace XGBoostTests {
  public static class TestUtils {
    public static float[][] GetClassifierDataTrain() {
      var trainCols = 4;
      var trainRows = 891;

      using (var parser = new TextFieldParser("../../test_files/train.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var dataTrain = new float[trainRows][];
        var row = 0;

        while (!parser.EndOfData) {
          dataTrain[row] = new float[trainCols - 1];
          var fields = parser.ReadFields();

          // skip label column in csv file
          for (var col = 1; col < fields.Length; col++)
            dataTrain[row][col - 1] = float.Parse(fields[col]);
          row += 1;
        }

        return dataTrain;
      }
    }

    public static float[] GetClassifierLabelsTrain() {
      var trainRows = 891;

      using (var parser = new TextFieldParser("../../test_files/train.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var labelsTrain = new float[trainRows];
        var row = 0;

        while (!parser.EndOfData) {
          var fields = parser.ReadFields();
          labelsTrain[row] = float.Parse(fields[0]);
          row += 1;
        }

        return labelsTrain;
      }
    }

    public static float[][] GetClassifierDataTest() {
      var testCols = 3;
      var testRows = 418;

      using (var parser = new TextFieldParser("../../test_files/test.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var dataTest = new float[testRows][];
        var row = 0;

        while (!parser.EndOfData) {
          dataTest[row] = new float[testCols];
          var fields = parser.ReadFields();

          for (var col = 0; col < fields.Length; col++)
            dataTest[row][col] = float.Parse(fields[col]);
          row += 1;
        }

        return dataTest;
      }
    }

    public static bool ClassifierPredsCorrect(float[] preds) {
      using (var parser = new TextFieldParser("../../test_files/predsclas.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var row = 0;
        var predInd = 0;

        while (!parser.EndOfData) {
          var fields = parser.ReadFields();

          for (var col = 0; col < fields.Length; col++) {
            var absDiff = Math.Abs(float.Parse(fields[col]) - preds[predInd]);
            if (absDiff > 0.01F)
              return false;
            predInd += 1;
          }
          row += 1;
        }
      }
      return true; // we haven't returned from a wrong prediction so everything is right
    }

    public static bool ClassifierPredsProbaCorrect(float[][] preds) {
      using (var parser = new TextFieldParser("../../test_files/predsclasproba.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var row = 0;

        while (!parser.EndOfData) {
          var fields = parser.ReadFields();

          for (var col = 0; col < fields.Length; col++) {
            var absDiff = Math.Abs(float.Parse(fields[col]) - preds[row][col]);
            if (absDiff > 0.0001F)
              return false;
          }
          row += 1;
        }
      }
      return true; // we haven't returned from a wrong prediction so everything is right
    }

    public static float[][] GetRegressorDataTrain() {
      var trainCols = 4;
      var trainRows = 891;

      using (var parser = new TextFieldParser("../../test_files//train.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var dataTrain = new float[trainRows][];
        var row = 0;

        while (!parser.EndOfData) {
          dataTrain[row] = new float[trainCols - 1];
          var fields = parser.ReadFields();

          // skip label column in csv file
          for (var col = 1; col < fields.Length; col++)
            dataTrain[row][col - 1] = float.Parse(fields[col]);
          row += 1;
        }

        return dataTrain;
      }
    }

    public static float[] GetRegressorLabelsTrain() {
      var trainRows = 891;

      using (var parser = new TextFieldParser("../../test_files//train.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var labelsTrain = new float[trainRows];
        var row = 0;

        while (!parser.EndOfData) {
          var fields = parser.ReadFields();
          labelsTrain[row] = float.Parse(fields[0]);
          row += 1;
        }

        return labelsTrain;
      }
    }

    public static float[][] GetRegressorDataTest() {
      var testCols = 3;
      var testRows = 418;

      using (var parser = new TextFieldParser("../../test_files//test.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var dataTest = new float[testRows][];
        var row = 0;

        while (!parser.EndOfData) {
          dataTest[row] = new float[testCols];
          var fields = parser.ReadFields();

          for (var col = 0; col < fields.Length; col++)
            dataTest[row][col] = float.Parse(fields[col]);
          row += 1;
        }

        return dataTest;
      }
    }

    public static bool RegressorPredsCorrect(float[] preds) {
      using (var parser = new TextFieldParser("../../test_files//predsreg.csv")) {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var row = 0;
        var predInd = 0;

        while (!parser.EndOfData) {
          var fields = parser.ReadFields();

          for (var col = 0; col < fields.Length; col++) {
            var absDiff = Math.Abs(float.Parse(fields[col]) - preds[predInd]);
            if (absDiff > 0.01F)
              return false;
            predInd += 1;
          }
          row += 1;
        }
      }
      return true; // we haven't returned from a wrong prediction so everything is right
    }

    public static bool AreEqual(float[][] arr1, float[][] arr2) {
      if (arr1.Length != arr2.Length) return false;
      if (arr1.Length == 0) return true;
      if(arr1[0].Length != arr2[0].Length) return false;
      for (var i = 0; i < arr1.Length; i++) for (var j = 0; j < arr1[i].Length; j++) if (arr1[i][j] != arr2[i][j]) return false;
      return true;
    }

    public static bool AreEqual(float[] arr, float[] other) {
      return arr.SequenceEqual(other);
    }
  }
}