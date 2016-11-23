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
      /*
      parameters["max_depth"] = maxDepth;
      parameters["learning_rate"] = learningRate;
      parameters["n_estimators"] = nEstimators;
      parameters["silent"] = silent;
      parameters["objective"] = objective;

      parameters["nthread"] = nThread;
      parameters["gamma"] = gamma;
      parameters["min_child_weight"] = minChildWeight;
      parameters["max_delta_step"] = maxDeltaStep;
      parameters["subsample"] = subsample;
      parameters["col_sample_by_tree"] = colSampleByTree;
      parameters["col_sample_by_level"] = colSampleByLevel;
      parameters["reg_alpha"] = regAlpha;
      parameters["reg_lambda"] = regLambda;
      parameters["scale_pos_weight"] = scalePosWeight;

      parameters["base_score"] = baseScore;
      parameters["seed"] = seed;
      parameters["missing"] = missing;
      parameters["_Booster"] = null;
      */
      SetParameter("max")
    }

    public void SetParameter(string name, string val)
    {

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
