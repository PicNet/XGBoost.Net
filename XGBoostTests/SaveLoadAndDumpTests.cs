using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XGBoost;

namespace XGBoostTests {
  [TestClass] public class SaveLoadAndDumpTests {
    private const string TEST_FILE = "tmpfile.tmp";

    [TestInitialize, TestCleanup] public void Reset() {
      if (File.Exists(TEST_FILE)) File.Delete(TEST_FILE);
    }

    [TestMethod] public void TestClassifierSaveAndLoad() {
      var dataTrain = TestUtils.GetClassifierDataTrain();
      var labelsTrain = TestUtils.GetClassifierLabelsTrain();
      var dataTest = TestUtils.GetClassifierDataTest();

      var xgbc = new XGBClassifier();
      xgbc.Fit(dataTrain, labelsTrain);
      
      var preds1 = xgbc.PredictProba(dataTest);
      xgbc.SaveModelToFile(TEST_FILE);

      var xgbc2 = BaseXgbModel.LoadClassifierFromFile(TEST_FILE);
      var preds2 = xgbc2.PredictProba(dataTest);
      Assert.IsTrue(TestUtils.AreEqual(preds1, preds2));
    }

    [TestMethod] public void TestRegressorSaveAndLoad() {
      var dataTrain = TestUtils.GetRegressorDataTrain();
      var labelsTrain = TestUtils.GetRegressorLabelsTrain();
      var dataTest = TestUtils.GetRegressorDataTest();

      var xgbr = new XGBRegressor();
      xgbr.Fit(dataTrain, labelsTrain);
      var preds1 = xgbr.Predict(dataTest);
      xgbr.SaveModelToFile(TEST_FILE);

      var xgbr2 = BaseXgbModel.LoadRegressorFromFile(TEST_FILE);
      var preds2 = xgbr2.Predict(dataTest);
      Assert.IsTrue(TestUtils.AreEqual(preds1, preds2));
    }    

    [TestMethod] public void TestClassifierSaveAndLoadWithParameters() {
      var dataTrain = TestUtils.GetClassifierDataTrain();
      var labelsTrain = TestUtils.GetClassifierLabelsTrain();
      var dataTest = TestUtils.GetClassifierDataTest();

      var xgbc = new XGBClassifier(10, 0.01f, 50);
      xgbc.Fit(dataTrain, labelsTrain);
      
      var preds1 = xgbc.PredictProba(dataTest);
      xgbc.SaveModelToFile(TEST_FILE);

      var xgbc2 = BaseXgbModel.LoadClassifierFromFile(TEST_FILE);
      var preds2 = xgbc2.PredictProba(dataTest);
      Assert.IsTrue(TestUtils.AreEqual(preds1, preds2));
    }

    [TestMethod] public void TestRegressorSaveAndLoadWithParameters() {
      var dataTrain = TestUtils.GetRegressorDataTrain();
      var labelsTrain = TestUtils.GetRegressorLabelsTrain();
      var dataTest = TestUtils.GetRegressorDataTest();

      var xgbr = new XGBRegressor();
      xgbr.Fit(dataTrain, labelsTrain);
      var preds1 = xgbr.Predict(dataTest);
      xgbr.SaveModelToFile(TEST_FILE);

      var xgbr2 = BaseXgbModel.LoadRegressorFromFile(TEST_FILE);
      var preds2 = xgbr2.Predict(dataTest);
      Assert.IsTrue(TestUtils.AreEqual(preds1, preds2));
    }  

    [TestMethod] public void TestClassifierDump() {
      var dataTrain = TestUtils.GetClassifierDataTrain();
      var labelsTrain = TestUtils.GetClassifierLabelsTrain();
      var dataTest = TestUtils.GetClassifierDataTest();

      var xgbc = new XGBClassifier();
      xgbc.Fit(dataTrain, labelsTrain);
      
      var preds1 = xgbc.PredictProba(dataTest);
      var description = xgbc.DumpModelEx();
      Console.WriteLine("Model Dumped: " + description);
    }
  }
}