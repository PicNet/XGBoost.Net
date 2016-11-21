using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace XGBoost
{
  public class DMatrix : IDisposable
  {
    private DMatrixHandle _handle;
    private string[] _featureNames;
    private string[] _featureTypes;

    public string[] FeatureNames
    {
      get { return _featureNames; }
      set { _featureNames = value; }
    }

    public string[] FeatureTypes
    {
      get { return _featureTypes; }
      set { _featureTypes = value; }
    }

    public float[] BaseMargin
    {
      get { return GetFloatInfo("base_margin"); }
      set { SetFloatInfo("base_margin", value); }
    }

    public float[] Label
    {
      get { return GetFloatInfo("label"); }
      set { SetFloatInfo("label", value); }
    }

    public float[] Weight
    {
      get { return GetFloatInfo("weight"); }
      set { SetFloatInfo("weight", value); }
    }

    public DMatrix(string dataPath = null, bool silent = false, 
                   string[] featureNames = null, string[] featureTypes = null)
    {
      if (dataPath == "")
      {
        return;
      }

      int output = DllMethods.XGDMatrixCreateFromFile(dataPath, silent ? 1 : 0, out _handle);
      if (output == -1) 
        throw new DllFailException("XGDMatrixCreateFromFile() in DMatrix() failed");
      FeatureNames = featureNames;
      FeatureTypes = featureTypes;
    }

    public float[] GetFloatInfo(string field)
    {
      ulong lenULong;
      IntPtr result;
      int output = DllMethods.XGDMatrixGetFloatInfo(_handle, field, out lenULong, out result);
      if (output == -1) 
        throw new DllFailException("XGDMatrixGetFloatInfo() in DMatrix.GetFloatInfo() failed");

      int len = unchecked((int)lenULong);
      float[] floatInfo = new float[len];
      for (int i = 0; i < len; i++)
      {
        byte[] floatBytes = new byte[4];
        floatBytes[0] = Marshal.ReadByte(result, 4*i + 0);
        floatBytes[1] = Marshal.ReadByte(result, 4*i + 1);
        floatBytes[2] = Marshal.ReadByte(result, 4*i + 2);
        floatBytes[3] = Marshal.ReadByte(result, 4*i + 3);
        float f = System.BitConverter.ToSingle(floatBytes, 0);
        floatInfo[i] = f;
      }
      return floatInfo;
    }

    public int NumCol()
    {
      ulong colsULong;
      int output = DllMethods.XGDMatrixNumCol(_handle, out colsULong);
      if (output == -1) 
        throw new DllFailException("XGDMatrixNumCol() in DMatrix.NumCol() failed");
      int cols = unchecked((int) colsULong);
      return cols;
    }

    public int NumRow()
    {
      ulong rowsULong;
      int output = DllMethods.XGDMatrixNumRow(_handle, out rowsULong);
      if (output == -1) 
        throw new DllFailException("XGDMatrixNumRow() in DMatrix.NumRow() failed");
      int rows = unchecked((int)rowsULong);
      return rows;
    }

    public void SaveBinary(string fname, bool silent = true)
    {
      int output = DllMethods.XGDMatrixSaveBinary(_handle, fname, silent ? 1 : 0);
      if (output == -1)
        throw new DllFailException("XGDMatrixSaveBinary() in DMatrix.SaveBinary() failed");
    }

    public void SetFloatInfo(string field, float[] floatInfo)
    {
      ulong len = (ulong)floatInfo.Length;
      int output = DllMethods.XGDMatrixSetFloatInfo(_handle, field, floatInfo, len);
      if (output == -1)
        throw new DllFailException("XGDMatrixSetFloatInfo() in DMatrix.SetFloatInfo() failed");
    }

    public void SetGroup(int[] group)
    {
      ulong len = (ulong)group.Length;
      int output = DllMethods.XGDMatrixSetGroup(_handle, group, len);
      if (output == -1)
        throw new DllFailException("XGDMatrixSetGroup() in DMatrix.SetGroup() failed");
    }

    public void Dispose()
    {
      if (_handle != null && !_handle.IsInvalid)
      {
        _handle.Dispose();
      }
    }
  }

  internal class DMatrixHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private DMatrixHandle()
        : base(true)
    {
    }

    override protected bool ReleaseHandle()
    {
      int output = DllMethods.XGDMatrixFree(handle);
      return output == 0 ? true : false;
    }
  }
}
