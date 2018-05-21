using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface ICalculationFunctions
    {
        float Minimum(List<float> listaPotrosnji);
        float Maximum(List<float> listaPotrosnji);
        float Average(List<float> listaPotrosnji);
    }
}
