using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisniSloj
{
    public class CalculationFunctions : ICalculationFunctions
    {
        public float Average(List<float> listaPotrosnji)
        {
            if (listaPotrosnji.Count == 0)
                return 0;
            return listaPotrosnji.Average();
        }

        public float Maximum(List<float> listaPotrosnji)
        {
            if (listaPotrosnji.Count == 0)
                return 0;
            return listaPotrosnji.Max();
        }

        public float Minimum(List<float> listaPotrosnji)
        {
            if (listaPotrosnji.Count == 0)
                return 0;
            return listaPotrosnji.Min();
        }
    }
}
