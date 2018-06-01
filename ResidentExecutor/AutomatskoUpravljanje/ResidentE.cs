using Contract;
using ServisniSloj;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatskoUpravljanje
{
    public class ResidentE
    {
        //public static List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>> listaUbacenihVrednostiWhile;


        ////public ResidentE()
        ////{
        ////    while(true)
        ////    {
        ////        Automatic()
        ////        Thread.Sleep(10000);
        ////    }
        ////}
        ////public Dictionary<int, float> Resident(ICalculationFunctions f)
        ////{
        ////    List<float> lista1 = new ObradaPodataka().GetVrijednosti();
        ////    List<int> lista2 = ObradaPodataka.GetIDFunkcija();
        ////    Dictionary<int, float> retVal = new Dictionary<int, float>();

        ////    for (int i = 0; i < lista2.Count; i++)
        ////    {
        ////        if (lista2[i] == 1)
        ////            retVal.Add(1, f.Average(lista1));
        ////        if (lista2[i] == 2)
        ////            retVal.Add(2, f.Maximum(lista1));
        ////        if (lista2[i] == 3)
        ////            retVal.Add(3, f.Minimum(lista1));
        ////    }

        ////    return retVal;
        ////}

        


        ////za svaku funkciju (int) za svako podrucje (string) njena vrednost (float)

        //public static Dictionary<Tuple<int, string>, Tuple<DateTime, float>> Automatic()
        //{
        //    Dictionary<Tuple<int, string>, Tuple<DateTime, float>> retVal = new Dictionary<Tuple<int, string>, Tuple<DateTime, float>>();
        //    ICalculationFunctions f = new CalculationFunctions();
        //    //funkcije koje su ukljucene
        //    List<int> listaIDFunkcija = ObradaPodataka.GetIDFunkcija();
        //    //List<string> listaIDGeoPodrucja = ObradaPodataka.GetVrijednostiZaGeoPodrucje().Keys.ToList();

        //    Dictionary<string, List<float>> vrednostiZaOdredjenoPodrucje = ObradaPodataka.GetVrijednostiZaGeoPodrucje();

        //    foreach (var item1 in listaIDFunkcija)
        //    {
        //        foreach (var item2 in vrednostiZaOdredjenoPodrucje)
        //        {
        //            if (item1 == 1)
        //                retVal.Add(new Tuple<int, string>(1, item2.Key), new Tuple<DateTime, float>(DateTime.Now,f.Average(item2.Value)));
        //            else if (item1 == 2)
        //                retVal.Add(new Tuple<int, string>(2, item2.Key), new Tuple<DateTime, float>(DateTime.Now, f.Maximum(item2.Value)));
        //            else
        //                retVal.Add(new Tuple<int, string>(3, item2.Key), new Tuple<DateTime, float>(DateTime.Now, f.Minimum(item2.Value)));
        //        }
        //    }

        //    return retVal;
        //}

        //public List<string> InsertIntoTable(List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>> listaUbacenihVrednostiWhile)
        //{
        //    //string unosUBazu = "INSERT INTO FunkcijaMaximum (IDGeoPodrucja, VremeProracuna, MaximalnaVrednost, PoslednjeVreme) VALUES ('DO', '2018-05-31 14:12:12.000', 234, '2018-05-31 12:31:56.000')";
        //    List<string> retVal = new List<string>();

        //    //Konekcija k = new Konekcija();

        //    //Vraca redove iz tabele sa danasnjim datumom i sortira listu po opadajucem redoslijedu
        //    //var movies = _db.Movies.OrderBy(c => c.Category).ThenBy(n => n.Name)
        //    List<PodaciIzBaze> traziDatum = Program.proxyDanasnji.DanasnjiDatum().OrderByDescending(o => o.Vreme).ThenBy(n => n.Vreme).ToList();
        //    List<PodaciIzBaze> traziNajnovijiDatum = SamoPoJednoPodrucje(traziDatum);

        //    foreach (var itemDatum in traziNajnovijiDatum)
        //    {
        //        foreach (var itemLista in listaUbacenihVrednostiWhile)
        //        {
        //            foreach (var itemDict in itemLista)
        //            {
        //                if (itemDict.Key.Item1 == 1 && itemDatum.ID == itemDict.Key.Item2)
        //                    retVal.Add("INSERT INTO FunkcijaAverage (IDGeoPodrucja, VremeProracuna, AverageVrednost, PoslednjeVreme) " +
        //                        "VALUES ('" + itemDict.Key.Item2 + "'," +
        //                        "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
        //                        itemDict.Value.Item2 + "," +
        //                        "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString()) + "'");

        //                else if (itemDict.Key.Item1 == 2 && itemDatum.ID == itemDict.Key.Item2)
        //                    retVal.Add("INSERT INTO FunkcijaMaximum (IDGeoPodrucja, VremeProracuna, MaximalnaVrednost, PoslednjeVreme) " +
        //                        "VALUES ('" + itemDict.Key.Item2 + "'," +
        //                        "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
        //                        itemDict.Value.Item2 + "," +
        //                        "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString()) + "'");

        //                else if (itemDict.Key.Item1 == 3 && itemDatum.ID == itemDict.Key.Item2)
        //                    retVal.Add("INSERT INTO FunkcijaMinimum (IDGeoPodrucja, VremeProracuna, MinimalnaVrednost, PoslednjeVreme) " +
        //                        "VALUES ('" + itemDict.Key.Item2 + "'," +
        //                        "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
        //                        itemDict.Value.Item2 + "," +
        //                        "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString()) + "'");
        //            }
        //        }
        //    }
        //    return retVal;
        //    //throw new NotImplementedException();
        //}

        //List<PodaciIzBaze> SamoPoJednoPodrucje(List<PodaciIzBaze> listaDatuma)
        //{
        //    List<PodaciIzBaze> retVal = listaDatuma;
        //    int i = 0;
        //    while (i < retVal.Count - 1)
        //    {
        //        if (retVal[i].ID == retVal[i + 1].ID)
        //            retVal.RemoveAt(i);
        //        else
        //            i++;
        //    }

        //    return retVal;
        //}

    }
}
