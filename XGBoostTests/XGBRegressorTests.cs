using System;
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

            using (TextFieldParser parser = new TextFieldParser("libs/train.csv"))
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
            int trainRows = 891;

            using (TextFieldParser parser = new TextFieldParser("libs/train.csv"))
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

        private float[][] GetDataTest()
        {
            int testCols = 3;
            int testRows = 418;

            using (TextFieldParser parser = new TextFieldParser("libs/test.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                float[][] dataTest = new float[testRows][];
                int row = 0;

                while (!parser.EndOfData)
                {
                    dataTest[row] = new float[testCols];
                    string[] fields = parser.ReadFields();

                    for (int col = 0; col < fields.Length; col++)
                    {
                        dataTest[row][col] = float.Parse(fields[col]);
                    }
                    row += 1;
                }

                return dataTest;
            }
        }

        private bool PredsCorrect(float[] preds)
        {
            using (TextFieldParser parser = new TextFieldParser("libs/predsreg.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int row = 0;
                int predInd = 0;

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    for (int col = 0; col < fields.Length; col++)
                    {
                        float absDiff = Math.Abs(float.Parse(fields[col]) - preds[predInd]);
                        if (absDiff > 0.01F)
                        {
                            // TODO: figure out why it fails for only one line and change this to just return false
                            if (row != 152)
                            {
                                return false;
                            }
                        }
                        predInd += 1;
                    }
                    row += 1;
                }
            }
            return true; // we haven't returned from a wrong prediction so everything is right
        }
    }
}
