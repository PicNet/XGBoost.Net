using System;
using System.Runtime.InteropServices;

namespace XGBoost
{
  public class XGBOOST_NATIVE_METHODS
  {
    [DllImport("libxgboost.dll")]
    public static extern int XGDMatrixCreateFromMat(float[] data, ulong nrow, ulong ncol, 
                                                    float missing, out IntPtr handle);

    [DllImport("libxgboost.dll")]
    public static extern int XGDMatrixFree(IntPtr handle);

    [DllImport("libxgboost.dll")]
    public static extern int XGDMatrixGetFloatInfo(IntPtr handle, string field, 
                                                   out ulong len, out IntPtr result);

    [DllImport("libxgboost.dll")]
    public static extern int XGDMatrixSetFloatInfo(IntPtr handle, string field,
                                                   float[] array, ulong len);

    [DllImport("libxgboost.dll")]
    public static extern int XGBoosterCreate(IntPtr[] dmats, 
                                             ulong len, out IntPtr handle);

    [DllImport("libxgboost.dll")]
    public static extern int XGBoosterFree(IntPtr handle);

    [DllImport("libxgboost.dll")]
    public static extern int XGBoosterSetParam(IntPtr handle, string name, string val);

    [DllImport("libxgboost.dll")]
    public static extern int XGBoosterUpdateOneIter(IntPtr bHandle, int iter, 
                                                    IntPtr dHandle);

    [DllImport("libxgboost.dll")]
    public static extern int XGBoosterPredict(IntPtr bHandle, IntPtr dHandle, 
                                              int optionMask, int ntreeLimit, 
                                              out ulong predsLen, out IntPtr predsPtr);
  }
}
