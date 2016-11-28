using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XGBoost;

namespace XGBoostTests
{
    [TestClass]
    public class XGBRegressorTests
    {
        /*
         * float[][] dataTrain = GetDataTrain();
            float[] labelsTrain = GetLabelsTrain();
            float[][] dataTest = GetDataTest();

            XGBRegressor xgbr = new XGBRegressor();
            xgbr.Fit(dataTrain, labelsTrain);
            float[] preds = xgbr.Predict(dataTest);
            Assert.IsTrue(PredsCorrect(preds));
            */

        [TestMethod]
        public void Predict()
        {
            float[][] dataTrain = GetDataTrain();
            float[] labelsTrain = GetLabelsTrain();
            float[][] dataTest = GetDataTest();

            Console.WriteLine("original");
            XGBRegressor xgbr = new XGBRegressor();
            xgbr.Fit(dataTrain, labelsTrain);
            float[] preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("max_depth");
            xgbr = new XGBRegressor(maxDepth: 1);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("learning_rate");
            xgbr = new XGBRegressor(learningRate: 0.5F);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("n_estimators");
            xgbr = new XGBRegressor(nEstimators: 10);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("silent");
            xgbr = new XGBRegressor(silent: false);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("objective");
            xgbr = new XGBRegressor(objective: "binary:logistic");
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("nthread");
            xgbr = new XGBRegressor(nThread: 1);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("gamma");
            xgbr = new XGBRegressor(gamma: 10);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("min_child_weight");
            xgbr = new XGBRegressor(minChildWeight: 10);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("max_delta_step");
            xgbr = new XGBRegressor(maxDeltaStep: 1);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("subsample");
            xgbr = new XGBRegressor(subsample: 0.01F);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("colsample_bytree");
            xgbr = new XGBRegressor(colSampleByTree: 0.9F);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("colsample_bylevel");
            xgbr = new XGBRegressor(colSampleByLevel: 1.0F);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("reg_alpha");
            xgbr = new XGBRegressor(regAlpha: 1);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("reg_lambda");
            xgbr = new XGBRegressor(regLambda: 0);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("scale_pos_weight");
            xgbr = new XGBRegressor(scalePosWeight: 10);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("base_score");
            xgbr = new XGBRegressor(baseScore: 1);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("seed");
            xgbr = new XGBRegressor(seed: 424242);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);

            Console.WriteLine("missing");
            xgbr = new XGBRegressor(missing: -42);
            xgbr.Fit(dataTrain, labelsTrain);
            preds = xgbr.Predict(dataTest);
            Console.WriteLine(preds[0]);
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
                            return false;
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
