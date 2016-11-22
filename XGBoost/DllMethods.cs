using System;
using System.Runtime.InteropServices;

namespace XGBoost
{
  class DllMethods
  {
    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixCreateFromMat(float[][] data, ulong nrow, ulong ncol, float missing, out DMatrixHandle handle);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixFree(IntPtr handle);
  }
}
