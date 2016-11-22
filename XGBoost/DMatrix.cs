using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace XGBoost
{
  public class DMatrix : IDisposable
  {
    private DMatrixHandle _handle;
    
    public DMatrixHandle Handle
    {
      get { return _handle; }
    }

    public float[] Label
    {
      get { return GetFloatInfo("label"); }
      set { SetFloatInfo("label", value); }
    }

    public DMatrix(float[][] data, float[] labels = null)
    {
      ulong nrows = unchecked((ulong)data.Length);
      ulong ncols = unchecked((ulong)data[0].Length);
      float missing = -1.0F; // abritrary value
      int output = DllMethods.XGDMatrixCreateFromMat(data, nrows, ncols, missing, out _handle);
      if (output == -1) 
        throw new DllFailException("XGDMatrixCreateFromMat() in DMatrix() failed");

      if (labels != null)
      {
        Label = labels;
      }
    }

    public float[] GetFloatInfo(string field)
    {
      ulong lenULong;
      IntPtr result;
      int output = DllMethods.XGDMatrixGetFloatInfo(_handle, field, out lenULong, out result);
      if (output == -1)
        throw new DllFailException("XGDMatrixGetFloatInfo() in DMatrix.GetFloatInfo() failed");

      int len = unchecked((int)lenULong);
      float[] floatInfo = new float[len];
      for (int i = 0; i < len; i++)
      {
        byte[] floatBytes = new byte[4];
        floatBytes[0] = Marshal.ReadByte(result, 4 * i + 0);
        floatBytes[1] = Marshal.ReadByte(result, 4 * i + 1);
        floatBytes[2] = Marshal.ReadByte(result, 4 * i + 2);
        floatBytes[3] = Marshal.ReadByte(result, 4 * i + 3);
        float f = System.BitConverter.ToSingle(floatBytes, 0);
        floatInfo[i] = f;
      }
      return floatInfo;
    }

    public void SetFloatInfo(string field, float[] floatInfo)
    {
      ulong len = (ulong)floatInfo.Length;
      int output = DllMethods.XGDMatrixSetFloatInfo(_handle, field, floatInfo, len);
      if (output == -1)
        throw new DllFailException("XGDMatrixSetFloatInfo() in DMatrix.SetFloatInfo() failed");
    }

    public void Dispose()
    {
      if (_handle != null && !_handle.IsInvalid)
      {
        _handle.Dispose();
      }
    }
  }

  public class DMatrixHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private DMatrixHandle()
        : base(true)
    {
    }

    override protected bool ReleaseHandle()
    {
      int output = DllMethods.XGDMatrixFree(handle);
      return output == 0 ? true : false;
    }
  }
}
