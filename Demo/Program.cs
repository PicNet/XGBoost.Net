using System;
using XGBoost;

namespace Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      DMatrix d = new DMatrix("libs/agaricus.txt.test");
      int cols = d.NumCol();
      Console.WriteLine("cols = " + cols.ToString());
      Console.ReadKey();
    }
  }
}
