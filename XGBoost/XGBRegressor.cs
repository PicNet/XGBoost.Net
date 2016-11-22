using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGBoost
{
  class XGBRegressor
  {
    private int _boostRounds = 10;
    private Booster _booster;

    public XGBRegressor()
    { 
    }

    public void Fit(float[][] data, float[] labels)
    {
      DMatrix dmat = new DMatrix(data, labels);
      _booster = Train(dmat);
    }

    public Booster Train(DMatrix dmat)
    {
      Booster booster = new Booster(dmat);
      for (int i = 0; i < _boostRounds; i++)
      {
        booster.Update(dmat, i);
      }
      return booster;
    }
  }
}
