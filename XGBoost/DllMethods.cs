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

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixNumCol(DMatrixHandle handle, out ulong cols);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixNumRow(DMatrixHandle handle, out ulong rows);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixGetFloatInfo(DMatrixHandle handle, string field, out ulong len, out IntPtr result);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixSetFloatInfo(DMatrixHandle handle, string field, float[] array, ulong len);

    [DllImport("libs/libxgboost.dll")]
    public static extern int XGDMatrixSaveBinary(DMatrixHandle handle,string fname, int silent);
  }
}
