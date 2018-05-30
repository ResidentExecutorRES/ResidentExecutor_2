using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServisniSloj
{
    public class IzvrsavanjeFunkcija
    {
        public List<int> GetIDFunkcija(List<FUNKCIJA> lista)
        {
            List<int> listaID = new List<int>();
            foreach (var item in lista)
            {
                if (item.UKLJUCENO == 1)
                {
                    listaID.Add(item.ID);
                }
            }
            return listaID;
        }

        //public Dictionary<int, float> IzracunajVrijednost()
        //{
        //    Dictionary<int, float> retVal = new Dictionary<int, float>();
        //    RadSaXML citanjeIzXML = new RadSaXML();
        //    List<FUNKCIJA> podaciIzXML = citanjeIzXML.CitajIzXML();
        //    ObradaPodataka obrada = new ObradaPodataka();
        //    ICalculationFunctions calculation = new CalculationFunctions();

        //    foreach (var item in GetIDFunkcija(podaciIzXML))
        //    {
        //        if (item == 1)
        //            retVal.Add(1, calculation.Average(obrada.GetVrijednosti()));
        //        else if (item == 2)
        //            retVal.Add(2, calculation.Maximum(obrada.GetVrijednosti()));
        //        else if (item == 3)
        //            retVal.Add(3, calculation.Minimum(obrada.GetVrijednosti()));
        //    }

        //    return retVal;
        //}
    }
}
