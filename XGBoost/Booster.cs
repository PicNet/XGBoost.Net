using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace XGBoost
{
  public class Booster : IDisposable
  {
    private BoosterHandle _handle;
    private const int NormalPrediction = 0;  // optionMask value for XGBoosterPredict

    public BoosterHandle Handle
    {
      get { return _handle; }
    }

    public Booster(DMatrix dTrain)
    {
      DMatrixHandle[] dmats = { dTrain.Handle };
      ulong len = unchecked((ulong)dmats.Length);
      int output = DllMethods.XGBoosterCreate(dmats, len, out _handle);
      if (output == -1)
        throw new DllFailException("XGBoosterCreate() in Booster() failed");
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
        floatBytes[0] = Marshal.ReadByte(predsPtr, 4 * i + 0);
        floatBytes[1] = Marshal.ReadByte(predsPtr, 4 * i + 1);
        floatBytes[2] = Marshal.ReadByte(predsPtr, 4 * i + 2);
        floatBytes[3] = Marshal.ReadByte(predsPtr, 4 * i + 3);
        float pred = System.BitConverter.ToSingle(floatBytes, 0);
        preds[i] = pred;
      }
      return preds;
    }

    public void Dispose()
    {
      if (_handle != null && !_handle.IsInvalid)
      {
        _handle.Dispose();
      }
    }
  }

  public class BoosterHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private BoosterHandle()
        : base(true)
    {
    }

    override protected bool ReleaseHandle()
    {
      int output = DllMethods.XGBoosterFree(handle);
      return output == 0 ? true : false;
    }
  }
}
