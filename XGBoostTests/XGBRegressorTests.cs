using Microsoft.VisualBasic.FileIO;
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
            int trainCols = 4;
            int trainRows = 891;

            using (TextFieldParser parser = new TextFieldParser(@"C:\dev\tests\testXgboost\simple_train.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                float[][] dataTrain = new float[trainRows][];
                int row = 0;

                while (!parser.EndOfData)
                {
                    dataTrain[row] = new float[trainCols - 1];
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
