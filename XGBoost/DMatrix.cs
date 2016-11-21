using System;
using Microsoft.Win32.SafeHandles;

namespace XGBoost
{
  public class DMatrix : IDisposable
  {
    private DMatrixHandle _handle;

    public DMatrix(string dataPath = null, bool silent = false)
    {
      if (dataPath == "")
      {
        return;
      }

      int output = DllMethods.XGDMatrixCreateFromFile(dataPath, silent ? 1 : 0, out _handle);
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
