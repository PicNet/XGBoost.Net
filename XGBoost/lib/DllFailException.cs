using System;

namespace XGBoost.lib
{
  class DllFailException : Exception
  {
    public DllFailException()
    {
    }

    public DllFailException(string message) : base(message)
    {
    }
  }
}
