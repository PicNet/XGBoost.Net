using System;
using System.Runtime.InteropServices;

namespace XGBoost.lib
{
  public class DMatrix : IDisposable
  {
    private bool disposed = false;
    private IntPtr _handle;
    private float Missing = -1.0F; // arbitrary value used to represent a missing value
    
    public IntPtr Handle
    {
      get { return _handle; }
    }

    public float[] Label
    {
      get { return GetFloatInfo("label"); }
      set { SetFloatInfo("label", value); }
    }

    public DMatrix(float[][] data, float[] labels = null)
        :this(Flatten2DArray(data), unchecked((ulong)data.Length), unchecked((ulong)data[0].Length), labels)
    {
    }

    public DMatrix(float[] data1D, ulong nrows, ulong ncols, float[] labels = null)
    {
        int output = XGBOOST_NATIVE_METHODS.XGDMatrixCreateFromMat(data1D, nrows, ncols, Missing, out _handle);
        if (output == -1)
            throw new DllFailException(XGBOOST_NATIVE_METHODS.XGBGetLastError());

        if (labels != null)
        {
            Label = labels;
        }
    }

    private static float[] Flatten2DArray(float[][] data2D)
    {
      int elementsNo = 0;
      for (int row = 0; row < data2D.Length; row++)
      {
        elementsNo += data2D[row].Length;
      }

      float[] data1D = new float[elementsNo];
      int ind = 0;
      for (int row = 0; row < data2D.Length; row++)
      {
        for (int col = 0; col < data2D[row].Length; col++)
        {
          data1D[ind] = data2D[row][col];
          ind += 1;
        }
      }
      return data1D;
    }

    private float[] GetFloatInfo(string field)
    {
      ulong lenULong;
      IntPtr result;
      int output = XGBOOST_NATIVE_METHODS.XGDMatrixGetFloatInfo(_handle, field, out lenULong, out result);
      if (output == -1)
        throw new DllFailException(XGBOOST_NATIVE_METHODS.XGBGetLastError());

      int len = unchecked((int)lenULong);
      float[] floatInfo = new float[len];
      for (int i = 0; i < len; i++)
      {
        byte[] floatBytes = new byte[4];
        floatBytes[0] = Marshal.ReadByte(result, 4 * i + 0);
        floatBytes[1] = Marshal.ReadByte(result, 4 * i + 1);
        floatBytes[2] = Marshal.ReadByte(result, 4 * i + 2);
        floatBytes[3] = Marshal.ReadByte(result, 4 * i + 3);
        float f = BitConverter.ToSingle(floatBytes, 0);
        floatInfo[i] = f;
      }
      return floatInfo;
    }

    private void SetFloatInfo(string field, float[] floatInfo)
    {
      ulong len = (ulong)floatInfo.Length;
      int output = XGBOOST_NATIVE_METHODS.XGDMatrixSetFloatInfo(_handle, field, floatInfo, len);
      if (output == -1)
        throw new DllFailException(XGBOOST_NATIVE_METHODS.XGBGetLastError());
    }

    // Dispose pattern from MSDN documentation
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // Dispose pattern from MSDN documentation
    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        int output = XGBOOST_NATIVE_METHODS.XGDMatrixFree(_handle);
        if (output == -1)
            throw new DllFailException(XGBOOST_NATIVE_METHODS.XGBGetLastError());

        disposed = true;
    }
  }
}
