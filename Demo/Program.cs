using System;
using XGBoost;

namespace Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      // load file from text file
      DMatrix d = new DMatrix("libs/agaricus.txt.test");

      // get how many columns and rows the DMatrix has
      int cols = d.NumCol();
      int rows = d.NumRow();

      float[] floatInfo = d.GetFloatInfo("label");

      Console.ReadKey();
    }
  }
}
