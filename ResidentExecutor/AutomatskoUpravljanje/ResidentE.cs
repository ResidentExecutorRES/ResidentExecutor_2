using Contract;
using ServisniSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatskoUpravljanje
{
    public class ResidentE
    {
        public static List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>> listaUbacenihVrednostiWhile;


        //public ResidentE()
        //{
        //    while(true)
        //    {
        //        Automatic()
        //        Thread.Sleep(10000);
        //    }
        //}
        //public Dictionary<int, float> Resident(ICalculationFunctions f)
        //{
        //    List<float> lista1 = new ObradaPodataka().GetVrijednosti();
        //    List<int> lista2 = ObradaPodataka.GetIDFunkcija();
        //    Dictionary<int, float> retVal = new Dictionary<int, float>();

        //    for (int i = 0; i < lista2.Count; i++)
        //    {
        //        if (lista2[i] == 1)
        //            retVal.Add(1, f.Average(lista1));
        //        if (lista2[i] == 2)
        //            retVal.Add(2, f.Maximum(lista1));
        //        if (lista2[i] == 3)
        //            retVal.Add(3, f.Minimum(lista1));
        //    }

        //    return retVal;
        //}

        


        //za svaku funkciju (int) za svako podrucje (string) njena vrednost (float)

        public static Dictionary<Tuple<int, string>, Tuple<DateTime, float>> Automatic()
        {
            Dictionary<Tuple<int, string>, Tuple<DateTime, float>> retVal = new Dictionary<Tuple<int, string>, Tuple<DateTime, float>>();
            ICalculationFunctions f = new CalculationFunctions();
            //funkcije koje su ukljucene
            List<int> listaIDFunkcija = ObradaPodataka.GetIDFunkcija();
            //List<string> listaIDGeoPodrucja = ObradaPodataka.GetVrijednostiZaGeoPodrucje().Keys.ToList();

            Dictionary<string, List<float>> vrednostiZaOdredjenoPodrucje = ObradaPodataka.GetVrijednostiZaGeoPodrucje();

            foreach (var item1 in listaIDFunkcija)
            {
                foreach (var item2 in vrednostiZaOdredjenoPodrucje)
                {
                    if (item1 == 1)
                        retVal.Add(new Tuple<int, string>(1, item2.Key), new Tuple<DateTime, float>(DateTime.Now,f.Average(item2.Value)));
                    else if (item1 == 2)
                        retVal.Add(new Tuple<int, string>(2, item2.Key), new Tuple<DateTime, float>(DateTime.Now, f.Maximum(item2.Value)));
                    else
                        retVal.Add(new Tuple<int, string>(3, item2.Key), new Tuple<DateTime, float>(DateTime.Now, f.Minimum(item2.Value)));
                }
            }

            return retVal;
        }
    }
}
