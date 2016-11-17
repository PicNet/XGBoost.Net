using System;

namespace XGBoost
{
  class DMatrix
  {
    private IntPtr dmatrixPtr;

    public DMatrix(string dataPath, bool silent = false)
    {
      int output = DllMethods.XGDMatrixCreateFromFile(dataPath, silent ? 1 : 0, out dmatrixPtr);
      if (output == -1) throw new DllFailException("XGDMatrixCreateFromFile() failed");
    }

    public string[] FeatureNames()
    {
      return null;
    }

    public string[] FeatureTypes()
    {
      return null;
    }

    public float GetBaseMargin()
    {
      return 0;
    }

    public float[][] GetFloatInfo()
    {
      return null;
    }

    public string[] GetLabel()
    {
      return null;
    }

    public float[][] GetUIntInfo()
    {
      return null;
    }

    public float[] GetWeight()
    {
      return null;
    }

    public int NumCol()
    {
      return 0;
    }

    public int NumRow()
    {
      return 0;
    }

    public void SaveBinary()
    {
    }

    public void SetBaseMargin()
    {
    }

    public void SetFloatInfo()
    {
    }

    public void SetGroup()
    {
    }

    public void SetLabel()
    {
    }

    public void SetUIntInfo()
    {
    }

    public void SetWeight()
    {
    }

    public DMatrix Slice()
    {
      return null;
    }
  }
}
