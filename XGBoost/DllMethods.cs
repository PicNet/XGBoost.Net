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

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixGetFloatInfo(DMatrixHandle handle, string field, out ulong len, out IntPtr result);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixSetFloatInfo(DMatrixHandle handle, string field, float[] array, ulong len);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGBoosterCreate(DMatrixHandle[] dmats, ulong len, out BoosterHandle handle);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGBoosterFree(IntPtr handle);
  }
}
