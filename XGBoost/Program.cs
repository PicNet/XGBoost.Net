using System;

namespace XGBoost
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Program.Main()");
      DMatrix d = new DMatrix("C:/Users/adamd/Documents/Visual Studio 2015/Projects/guidoXgboost/guidoXgboost/libs/test.txt");
      int cols = d.NumCol();
      Console.WriteLine("cols = " + cols.ToString());
      Console.ReadKey();
    }
  }
}
