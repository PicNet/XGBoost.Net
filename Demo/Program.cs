using System;
using XGBoost;
using Microsoft.VisualBasic.FileIO;

namespace Demo
{
    class Program
    {
        static int trainCols = 4;
        static int trainRows = 891;
        static int testCols = 3;
        static int testRows = 418;

        static void Main(string[] args)
        {
            float[][] dataTrain = GetDataTrain();
            float[] labelsTrain = GetLabelsTrain();
            float[][] dataTest = GetDataTest();

            XGBRegressor xgbr = new XGBRegressor();
            xgbr.Fit(dataTrain, labelsTrain);
            float[] preds = xgbr.Predict(dataTest);
            PrintPreds(preds);
            Console.WriteLine();

            XGBClassifier xgbc = new XGBClassifier();
            xgbc.Fit(dataTrain, labelsTrain);
            preds = xgbc.Predict(dataTest);
            PrintPreds(preds);
            Console.WriteLine();

            xgbc = new XGBClassifier();
            xgbc.Fit(dataTrain, labelsTrain);
            float[][] predsProba = xgbc.PredictProba(dataTest);
            PrintPredsProba(predsProba);
            Console.ReadKey();
        }

        static float[][] GetDataTrain()
        {
            using (TextFieldParser parser = new TextFieldParser("lib/simple_train.csv"))
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

        static float[] GetLabelsTrain()
        {
            using (TextFieldParser parser = new TextFieldParser("lib/simple_train.csv"))
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
            using (TextFieldParser parser = new TextFieldParser("lib/simple_test.csv"))
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

        static void PrintPreds(float[] preds)
        {
            for (int i = 0; i < preds.Length; i++)
            {
                Console.Write(preds[i].ToString() + " ");
            }
            Console.WriteLine();
        }

        static void PrintPredsProba(float[][] predsProba)
        {
            for (int i = 0; i < predsProba.Length; i++)
            {
                Console.Write(predsProba[i][0].ToString() + " " +
                              predsProba[i][1].ToString() + " ");
            }
            Console.WriteLine();
        }
    }
}
