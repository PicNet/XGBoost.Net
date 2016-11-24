using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace XGBoost
{
  public class Booster : IDisposable
  {
    private bool disposed = false;
    private IntPtr _handle;
    private const int NormalPrediction = 0;  // optionMask value for XGBoosterPredict

    public IntPtr Handle
    {
      get { return _handle; }
    }

    public Booster(IDictionary<string, object> parameters, DMatrix dTrain)
    {
      IntPtr[] dmats = { dTrain.Handle };
      ulong len = unchecked((ulong)dmats.Length);
      int output = DllMethods.XGBoosterCreate(dmats, len, out _handle);
      if (output == -1)
        throw new DllFailException("XGBoosterCreate() in Booster() failed");
      SetParameters(parameters);
      //PrintParameters(parameters); // TODO: remove after debugging done
    }

    public void Update(DMatrix dTrain, int iter)
    {
      int output = DllMethods.XGBoosterUpdateOneIter(Handle, iter, dTrain.Handle);
      if (output == -1)
        throw new DllFailException("XGBoosterUpdateOneIter() in Booster.Update() failed");
    }

    public float[] Predict(DMatrix dTest)
    {
      ulong predsLen;
      IntPtr predsPtr;
      int output = DllMethods.XGBoosterPredict(_handle, dTest.Handle, NormalPrediction, 0,
                                               out predsLen, out predsPtr);
      if (output == -1)
        throw new DllFailException("XGBoosterPredict() in Booster.Predict() failed");
      
      float[] preds = GetPredictionsArray(predsPtr, predsLen);
      return preds;
    }

    public float[] GetPredictionsArray(IntPtr predsPtr, ulong predsLen)
    {
      int predsNo = unchecked((int)predsLen);
      float[] preds = new float[predsNo];
      for (int i = 0; i < predsNo; i++)
      {
        byte[] floatBytes = new byte[4];
        for (int b = 0; b < 4; b++)
        {
          floatBytes[b] = Marshal.ReadByte(predsPtr, 4*i + b);
        }
        float pred = BitConverter.ToSingle(floatBytes, 0);
        preds[i] = pred;
      }
      return preds;
    }

    public void SetParameters(IDictionary<string, Object> parameters)
    {
      SetParameter("max_depth", ((int)parameters["max_depth"]).ToString());
      SetParameter("learning_rate", ((float)parameters["learning_rate"]).ToString());
      SetParameter("n_estimators", ((int)parameters["n_estimators"]).ToString());
      SetParameter("silent", ((bool)parameters["silent"]).ToString());
      SetParameter("objective", ((string)parameters["objective"]).ToString());

      SetParameter("nthread", ((int)parameters["nthread"]).ToString());
      SetParameter("gamma", ((float)parameters["gamma"]).ToString());
      SetParameter("min_child_weight", ((int)parameters["min_child_weight"]).ToString());
      SetParameter("max_delta_step", ((int)parameters["max_delta_step"]).ToString());
      SetParameter("subsample", ((float)parameters["subsample"]).ToString());
      SetParameter("colsample_bytree", ((float)parameters["colsample_bytree"]).ToString());
      SetParameter("colsample_bylevel", ((float)parameters["colsample_bylevel"]).ToString());
      SetParameter("reg_alpha", ((float)parameters["reg_alpha"]).ToString());
      SetParameter("reg_lambda", ((float)parameters["reg_lambda"]).ToString());
      SetParameter("scale_pos_weight", ((float)parameters["scale_pos_weight"]).ToString());

      SetParameter("base_score", ((float)parameters["base_score"]).ToString());
      SetParameter("seed", ((int)parameters["seed"]).ToString());
      SetParameter("missing", ((float)parameters["missing"]).ToString());
    }

    public void PrintParameters(IDictionary<string, Object> parameters)
    {
      Console.WriteLine("max_depth: " + ((int)parameters["max_depth"]).ToString());
      Console.WriteLine("learning_rate: " + ((float)parameters["learning_rate"]).ToString());
      Console.WriteLine("n_estimators: " + ((int)parameters["n_estimators"]).ToString());
      Console.WriteLine("silent: " + ((bool)parameters["silent"]).ToString());
      Console.WriteLine("objective: " + ((string)parameters["objective"]).ToString());

      Console.WriteLine("nthread: " + ((int)parameters["nthread"]).ToString());
      Console.WriteLine("gamma: " + ((float)parameters["gamma"]).ToString());
      Console.WriteLine("min_child_weight: " + ((int)parameters["min_child_weight"]).ToString());
      Console.WriteLine("max_delta_step: " + ((int)parameters["max_delta_step"]).ToString());
      Console.WriteLine("subsample: " + ((float)parameters["subsample"]).ToString());
      Console.WriteLine("colsample_bytree: " + ((float)parameters["colsample_bytree"]).ToString());
      Console.WriteLine("colsample_bylevel: " + ((float)parameters["colsample_bylevel"]).ToString());
      Console.WriteLine("reg_alpha: " + ((float)parameters["reg_alpha"]).ToString());
      Console.WriteLine("reg_lambda: " + ((float)parameters["reg_lambda"]).ToString());
      Console.WriteLine("scale_pos_weight: " + ((float)parameters["scale_pos_weight"]).ToString());

      Console.WriteLine("base_score: " + ((float)parameters["base_score"]).ToString());
      Console.WriteLine("seed: " + ((int)parameters["seed"]).ToString());
      Console.WriteLine("missing: " + ((float)parameters["missing"]).ToString());
    }

    public void SetParameter(string name, string val)
    {
      int output = DllMethods.XGBoosterSetParam(_handle, name, val);
      if (output == -1)
        throw new DllFailException("XGBoosterSetParam() in Booster.SetParameter() failed");
    }

    // Dispose pattern from MSDN documentation
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // Dispose pattern from MSDN documentation
    protected virtual void Dispose(bool disposing)
    {
      if (disposed)
        return;
      DllMethods.XGDMatrixFree(_handle);
      disposed = true;
    }
  }
}
