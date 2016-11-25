using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XGBoost;

namespace XGBoostTests
{
    [TestClass]
    public class XGBRegressorTests
    {
        [TestMethod]
        public void Predict()
        {
            float[][] dataTrain = GetDataTrain();
            float[] labelsTrain = GetLabelsTrain();
            float[][] dataTest = GetDataTest();

            XGBRegressor xgbr = new XGBRegressor();
            xgbr.Fit(dataTrain, labelsTrain);
            float[] preds = xgbr.Predict(dataTest);
            Assert.IsTrue(PredsCorrect(preds));
        }

        private float[][] GetDataTrain()
        {
            return null;
        }

        private float[] GetLabelsTrain()
        {
            return null;
        }

        private float[][] GetDataTest()
        {
            return null;
        }

        private bool PredsCorrect(float[] preds)
        {
            return true;
        }
    }
}
