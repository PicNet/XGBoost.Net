using System;

namespace XGBoost
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
