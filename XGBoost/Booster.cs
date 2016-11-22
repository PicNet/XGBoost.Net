using System;
using Microsoft.Win32.SafeHandles;

namespace XGBoost
{
  public class Booster : IDisposable
  {
    private BoosterHandle _handle;

    public BoosterHandle Handle
    {
      get { return _handle; }
    }

    public Booster(DMatrix dmat)
    {
      DMatrixHandle[] dmats = { dmat.Handle };
      ulong len = unchecked((ulong)dmats.Length);
      int output = DllMethods.XGBoosterCreate(dmats, len, out _handle);
      if (output == -1)
        throw new DllFailException("XGBoosterCreate() in Booster() failed");
    }

    public void Update(DMatrix dmat, int iter)
    {
      DllMethods.XGBoosterUpdateOneIter(Handle, iter, dmat.Handle);
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
