using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PristupDB;

namespace ServisniSloj
{
    public class ObradaPodataka
    {
        //Fja koja uzima samo vrijednosti iz Liste List<PodaciIzBaze> i prosledjuje f-jama za racunanje Min, Max i Avg
        //Vraca za danasnji datum id podrucja i vrenost

            /// <summary>
            /// za danasnji dan za svaki ID lista vrednosti iz tabele
            /// </summary>
            /// <returns></returns>
        public static Dictionary<string, List<float>> GetVrijednostiZaGeoPodrucje()
        {
            Dictionary<string, List<float>> ret = new Dictionary<string, List<float>>();
            List<float> ret2 = new List<float>();
            Konekcija k = new Konekcija();

            List<PodaciIzBaze> podaci = k.DanasnjiDatum();
            foreach (var item1 in GetIDGeoPodrucja())
            {

                foreach (var item2 in podaci)
                {
                    if (item1 == item2.ID)
                        ret2.Add(item2.Vrednost);
                }

                ret.Add(item1, ret2);
                ret2 = new List<float>();
            }          
            
            return ret;
        }
        public static  List<int> GetIDFunkcija()
        {
            List<int> listaID = new List<int>();
            foreach (var item in (new RadSaXML().CitajIzXML()))
            {
                if (item.UKLJUCENO == 1)
                {
                    listaID.Add(item.ID);
                }
            }
            return listaID;
        }

        public static List<string> GetIDGeoPodrucja()
        {
            List<string> listaID = new List<string>();
            foreach (var item in (new RadSaXML().CitajIzXMLGeoPodrucja()))
            {
                listaID.Add(item.Id);
            }
            return listaID;
        }
    }
}
