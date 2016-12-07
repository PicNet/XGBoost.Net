using System;
using System.Runtime.InteropServices;

namespace XGBoost
{
  public class XGBOOST_NATIVE_METHODS
  {
    // use first dllLocation for development
    // use second dllLocation for publishing (making sure it refers to correct next version of package)
    //private const string dllLocation = "libxgboost.dll";
    private const string dllLocation = "../../../packages/PicNet.XGBoost.0.1.0/lib/libxgboost.dll";

    [DllImport(dllLocation)]
    public static extern string XGBGetLastError();

    [DllImport(dllLocation)]
    public static extern int XGDMatrixCreateFromMat(float[] data, ulong nrow, ulong ncol, 
                                                    float missing, out IntPtr handle);

    [DllImport(dllLocation)]
    public static extern int XGDMatrixFree(IntPtr handle);

    [DllImport(dllLocation)]
    public static extern int XGDMatrixGetFloatInfo(IntPtr handle, string field, 
                                                   out ulong len, out IntPtr result);

    [DllImport(dllLocation)]
    public static extern int XGDMatrixSetFloatInfo(IntPtr handle, string field,
                                                   float[] array, ulong len);

    [DllImport(dllLocation)]
    public static extern int XGBoosterCreate(IntPtr[] dmats, 
                                             ulong len, out IntPtr handle);

    [DllImport(dllLocation)]
    public static extern int XGBoosterFree(IntPtr handle);

    [DllImport(dllLocation)]
    public static extern int XGBoosterSetParam(IntPtr handle, string name, string val);

    [DllImport(dllLocation)]
    public static extern int XGBoosterUpdateOneIter(IntPtr bHandle, int iter, 
                                                    IntPtr dHandle);

    [DllImport(dllLocation)]
    public static extern int XGBoosterPredict(IntPtr bHandle, IntPtr dHandle, 
                                              int optionMask, int ntreeLimit, 
                                              out ulong predsLen, out IntPtr predsPtr);
  }
}
