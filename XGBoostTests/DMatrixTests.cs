using Microsoft.VisualStudio.TestTools.UnitTesting;
using XGBoost;

namespace XGBoostTests
{
  [TestClass]
  public class DMatrixTests
  {
    [TestMethod]
    public void NumCol()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      int cols = d.NumCol();
      Assert.AreEqual(cols, 127);
    }

    [TestMethod]
    public void NumRow()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      int rows = d.NumRow();
      Assert.AreEqual(rows, 1611);
    }

    [TestMethod]
    public void GetFloatInfo()
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      
      float[] labelInfo = d.GetFloatInfo("label");
      float[] first5ActualLabelInfo = { 0, 1, 0, 0, 0 };
      for (int i = 0; i < first5ActualLabelInfo.Length; i++)
      {
        string errorMsg = "Labels at index " + i.ToString() + " are not equal";
        Assert.AreEqual(labelInfo[i], first5ActualLabelInfo[i], 0.01, errorMsg);
      }

      float[] weightInfo = d.GetFloatInfo("weight");
      float[] actualWeightInfo = { };
      // both should be empty
      Assert.AreEqual(weightInfo.Length, actualWeightInfo.Length); 

      float[] baseMariginInfo = d.GetFloatInfo("base_margin");
      float[] actualBaseMarginInfo = { };
      // both should be empty
      Assert.AreEqual(baseMariginInfo.Length, actualBaseMarginInfo.Length); 
    }
  }
}
