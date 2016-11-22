using System;
using Microsoft.Win32.SafeHandles;

namespace XGBoost
{
  public class DMatrix : IDisposable
  {
    private DMatrixHandle _handle;

    public DMatrix(float[][] data)
    {
      ulong nrows = unchecked((ulong)data.Length);
      ulong ncols = unchecked((ulong)data[0].Length);
      float missing = -1.0F; // abritrary value
      int output = DllMethods.XGDMatrixCreateFromMat(data, nrows, ncols, missing, out _handle);
      if (output == -1) 
        throw new DllFailException("XGDMatrixCreateFromFile() in DMatrix() failed");
    }

    public void Dispose()
    {
      if (_handle != null && !_handle.IsInvalid)
      {
        _handle.Dispose();
      }
    }
  }

  internal class DMatrixHandle : SafeHandleZeroOrMinusOneIsInvalid
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
