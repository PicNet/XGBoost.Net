using System;
using Microsoft.Win32.SafeHandles;

namespace XGBoost
{
  public class Booster : IDisposable
  {
    private BoosterHandle _handle;

    public Booster(float[][] data)
    {
      
    }

    public void Dispose()
    {
      if (_handle != null && !_handle.IsInvalid)
      {
        _handle.Dispose();
      }
    }
  }

  internal class BoosterHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private BoosterHandle()
        : base(true)
    {
    }

    override protected bool ReleaseHandle()
    {
      return true;
    }
  }
}
