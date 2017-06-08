using XGBoost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XGBoostTests
{
    [TestClass]
    public class XGBClassifierTests
    {
      [TestMethod]
      public void Predict()
      {
        var dataTrain = TestUtils.GetClassifierDataTrain();
        var labelsTrain = TestUtils.GetClassifierLabelsTrain();
        var dataTest = TestUtils.GetClassifierDataTest();

        var xgbc = new XGBClassifier();
        xgbc.Fit(dataTrain, labelsTrain);
        var preds = xgbc.Predict(dataTest);
        Assert.IsTrue(TestUtils.ClassifierPredsCorrect(preds));
      }

      [TestMethod]
      public void PredictProba()
      {
        var dataTrain = TestUtils.GetClassifierDataTrain();
        var labelsTrain = TestUtils.GetClassifierLabelsTrain();
        var dataTest = TestUtils.GetClassifierDataTest();

        var xgbc = new XGBClassifier();
        xgbc.Fit(dataTrain, labelsTrain);
        var preds = xgbc.PredictProba(dataTest);
        Assert.IsTrue(TestUtils.ClassifierPredsProbaCorrect(preds));
      }        
  }
}
