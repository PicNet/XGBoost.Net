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
  }
}
