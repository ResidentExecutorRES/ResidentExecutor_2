﻿using Contract;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupDB
{
    public class InsertInto : IInsert
    {
        public List<string> InsertIntoTable(List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>> listaUbacenihVrednostiWhile)
        {
            //string unosUBazu = "INSERT INTO FunkcijaMaximum (IDGeoPodrucja, VremeProracuna, MaximalnaVrednost, PoslednjeVreme) VALUES ('DO', '2018-05-31 14:12:12.000', 234, '2018-05-31 12:31:56.000')";
            List<string> retVal = new List<string>();

            Konekcija k = new Konekcija();

            //Vraca redove iz tabele sa danasnjim datumom i sortira listu po opadajucem redoslijedu
            //var movies = _db.Movies.OrderBy(c => c.Category).ThenBy(n => n.Name)
            List<PodaciIzBaze> traziDatum = k.DanasnjiDatum().OrderByDescending(o=>o.Vreme).ThenBy(n=>n.Vreme).ToList();
            List<PodaciIzBaze> traziNajnovijiDatum = SamoPoJednoPodrucje(traziDatum);

            foreach (var itemDatum in traziNajnovijiDatum)
            {
                foreach (var itemLista in listaUbacenihVrednostiWhile)
                {
                    foreach (var itemDict in itemLista)
                    {
                        if (itemDict.Key.Item1 == 1 && itemDatum.ID == itemDict.Key.Item2)
                            retVal.Add("INSERT INTO FunkcijaAverage (IDGeoPodrucja, VremeProracuna, AverageVrednost, PoslednjeVreme) " +
                                "VALUES ('" + itemDict.Key.Item2 + "'," +
                                "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
                                itemDict.Value.Item2 + "," +
                                "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString())+"')");

                        else if (itemDict.Key.Item1 == 2 && itemDatum.ID == itemDict.Key.Item2)
                            retVal.Add("INSERT INTO FunkcijaMaximum (IDGeoPodrucja, VremeProracuna, MaximalnaVrednost, PoslednjeVreme) " +
                                "VALUES ('" + itemDict.Key.Item2 + "'," +
                                "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
                                itemDict.Value.Item2 + "," +
                                "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString()) + "')");
                        
                        else if (itemDict.Key.Item1 == 3 && itemDatum.ID == itemDict.Key.Item2)
                            retVal.Add("INSERT INTO FunkcijaMinimum (IDGeoPodrucja, VremeProracuna, MinimalnaVrednost, PoslednjeVreme) " +
                                "VALUES ('" + itemDict.Key.Item2 + "'," +
                                "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
                                itemDict.Value.Item2 + "," +
                                "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString()) + "')");
                    }
                }
            }
            return retVal;
            //throw new NotImplementedException();
        }

        List<PodaciIzBaze> SamoPoJednoPodrucje(List<PodaciIzBaze> listaDatuma)
        {
            List<PodaciIzBaze> retVal = listaDatuma;
            int i = 0;
            while(i < retVal.Count - 1)
            {
                if (retVal[i].ID == retVal[i + 1].ID)
                    retVal.RemoveAt(i);
                else
                    i++;
            }

            return retVal;
        }


    }
}
