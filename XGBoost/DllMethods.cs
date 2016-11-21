using System;
using System.Runtime.InteropServices;

namespace XGBoost
{
  class DllMethods
  {
    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixCreateFromFile([MarshalAs(UnmanagedType.LPStr)] string dataPath, int silent, out DMatrixHandle handle);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixFree(IntPtr handle);
  }
}
