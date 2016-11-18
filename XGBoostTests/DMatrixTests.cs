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
      float[] first5ActualLabels = { 0, 1, 0, 0, 0 };
      for (int i = 0; i < first5ActualLabels.Length; i++)
      {
        string errorMsg = "Labels number " + i.ToString() + " are not equal";
        Assert.AreEqual(labelInfo[i], first5ActualLabels[i], 0.01, errorMsg);
      }
    }
  }
}
