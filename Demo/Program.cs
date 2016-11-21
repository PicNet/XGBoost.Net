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

      Console.ReadKey();
    }
  }
}
