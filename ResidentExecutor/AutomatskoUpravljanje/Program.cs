﻿using Contract;
using KorisnickiInterfejs;
using PristupDB;
using ServisniSloj;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatskoUpravljanje
{
    class Program
    {
        public static ChannelFactory<IInsert> factory = null;
        public static ChannelFactory<IDanasnjiDatum> factoryDanasnji = null;
        public static IInsert proxy = null;

        public static IDanasnjiDatum proxyDanasnji = null;

        public static ChannelFactory<IProveriPreInsert> factoryPreInserta = null;
        public static IProveriPreInsert proxyProveriPreInserta = null;

        static void Main(string[] args)
        {

            Connect();

            while (true)
            {

                // List<string> saljiUBazu = proxyDanasnji.InsertIntoTable(ResidentE.listaUbacenihVrednostiWhile);

                listaUbacenihVrednostiWhile = new List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>>();

               List<PodaciIzBaze> podaci = proxyDanasnji.DanasnjiDatum();

                listaUbacenihVrednostiWhile.Add(Automatic(podaci));


                foreach (var item in listaUbacenihVrednostiWhile)
                {
                    foreach (var item1 in item)
                    {
                        Console.WriteLine(item1.Key.Item1 + " " + item1.Key.Item2 + " " + item1.Value.Item1 + " " + item1.Value.Item2);
                    }
                }


                List<string> uBazu = InsertIntoTable(listaUbacenihVrednostiWhile);

                DuplexSample(uBazu);

                podaci = new List<PodaciIzBaze>();

                Thread.Sleep(20000);
            }

           // Console.ReadKey();

            //Disconnect();
        }

        public static void Connect()
        {
            //factory = new ChannelFactory<IInsert>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10101/IInsert"));
            factoryDanasnji = new ChannelFactory<IDanasnjiDatum>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10103/IDanasnjiDatum"));
            NetTcpBinding binding = new NetTcpBinding
            {
                ReceiveTimeout = TimeSpan.FromMinutes(1)
            };
            factoryPreInserta = new ChannelFactory<IProveriPreInsert>(binding, new EndpointAddress("net.tcp://localhost:10105/IProveriPreInsert"));
            //proxy = factory.CreateChannel();

            proxyDanasnji = factoryDanasnji.CreateChannel();
            proxyProveriPreInserta = factoryPreInserta.CreateChannel();
        }

        public static void Disconnect()
        {
            //proxy = null;
            //factory.Close();

            proxyDanasnji = null;
            factoryDanasnji.Close();

            proxyProveriPreInserta = null;
            factoryPreInserta.Close();
        }

        public static List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>> listaUbacenihVrednostiWhile;
        //new List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>>()
        //{
        //    //new Dictionary<Tuple<int, string>, Tuple<DateTime, float>>(Automatic())
        //};

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

        public static Dictionary<Tuple<int, string>, Tuple<DateTime, float>> Automatic(List<PodaciIzBaze> podaci)
        {
            Dictionary<Tuple<int, string>, Tuple<DateTime, float>> retVal = new Dictionary<Tuple<int, string>, Tuple<DateTime, float>>();
            ICalculationFunctions f = new CalculationFunctions();
            //funkcije koje su ukljucene
            List<int> listaIDFunkcija = ObradaPodataka.GetIDFunkcija();
            //List<string> listaIDGeoPodrucja = ObradaPodataka.GetVrijednostiZaGeoPodrucje().Keys.ToList();

            Dictionary<string, List<float>> vrednostiZaOdredjenoPodrucje = ObradaPodataka.GetVrijednostiZaGeoPodrucje(podaci);

            foreach (var item1 in listaIDFunkcija)
            {
                foreach (var item2 in vrednostiZaOdredjenoPodrucje)
                {
                    if (item1 == 1)
                        retVal.Add(new Tuple<int, string>(1, item2.Key), new Tuple<DateTime, float>(DateTime.Now, f.Average(item2.Value)));
                    else if (item1 == 2)
                        retVal.Add(new Tuple<int, string>(2, item2.Key), new Tuple<DateTime, float>(DateTime.Now, f.Maximum(item2.Value)));
                    else
                        retVal.Add(new Tuple<int, string>(3, item2.Key), new Tuple<DateTime, float>(DateTime.Now, f.Minimum(item2.Value)));
                }
            }

            return retVal;
        }

        public static List<string> InsertIntoTable(List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>> listaUbacenihVrednostiWhile)
        {
            //string unosUBazu = "INSERT INTO FunkcijaMaximum (IDGeoPodrucja, VremeProracuna, MaximalnaVrednost, PoslednjeVreme) VALUES ('DO', '2018-05-31 14:12:12.000', 234, '2018-05-31 12:31:56.000')";
            List<string> retVal = new List<string>();

            //Konekcija k = new Konekcija();

            //Vraca redove iz tabele sa danasnjim datumom i sortira listu po opadajucem redoslijedu
            //var movies = _db.Movies.OrderBy(c => c.Category).ThenBy(n => n.Name)
            List<PodaciIzBaze> traziDatum = Program.proxyDanasnji.DanasnjiDatum().OrderByDescending(o => o.ID).ThenBy(n => n.Vreme).ToList();
            List<PodaciIzBaze> traziNajnovijiDatum = SamoPoJednoPodrucje(traziDatum);

            foreach (var itemDatum in traziNajnovijiDatum)
            {
                foreach (var itemLista in listaUbacenihVrednostiWhile)
                {
                    foreach (var itemDict in itemLista)
                    {
                        if (itemDict.Key.Item1 == 1 && itemDatum.ID == itemDict.Key.Item2 && (ProveriZaIf(itemDict.Key.Item2, SqlDateTime.Parse(itemDatum.Vreme.ToString()), itemDict.Key.Item1)))
                            retVal.Add("INSERT INTO FunkcijaAverage (IDGeoPodrucja, VremeProracuna, AverageVrednost, PoslednjeVreme) " +
                                "VALUES ('" + itemDict.Key.Item2 + "'," +
                                "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
                                itemDict.Value.Item2 + "," +
                                "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString()) + "')");

                        else if (itemDict.Key.Item1 == 2 && itemDatum.ID == itemDict.Key.Item2 && (ProveriZaIf(itemDict.Key.Item2, SqlDateTime.Parse(itemDatum.Vreme.ToString()), itemDict.Key.Item1)))
                            retVal.Add("INSERT INTO FunkcijaMaximum (IDGeoPodrucja, VremeProracuna, MaximalnaVrednost, PoslednjeVreme) " +
                                "VALUES ('" + itemDict.Key.Item2 + "'," +
                                "'" + SqlDateTime.Parse(itemDict.Value.Item1.ToString()) + "'," +
                                itemDict.Value.Item2 + "," +
                                "'" + SqlDateTime.Parse(itemDatum.Vreme.ToString()) + "')");

                        else if (itemDict.Key.Item1 == 3 && itemDatum.ID == itemDict.Key.Item2 && (ProveriZaIf(itemDict.Key.Item2, SqlDateTime.Parse(itemDatum.Vreme.ToString()), itemDict.Key.Item1)))
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

        public static bool ProveriZaIf(string id, SqlDateTime vreme, int i)
        {
            string spojeno = id + vreme.ToString();

            bool retVal = true;

            Dictionary<string, List<CalculationTable>> p =  proxyProveriPreInserta.PosaljiInsert();

            foreach (var item in p)
            {
                foreach (var item2 in item.Value)
                {
                    if (i == 1 && item.Key == "AVG" && (item2.Id + item2.PoslednjeVreme.ToString().ToString())==spojeno)
                    {
                        retVal = false;
                        break;
                    }
                    else if (i == 2 && item.Key == "MAX" && (item2.Id + item2.PoslednjeVreme.ToString().ToString()) == spojeno)
                    {
                        retVal = false;
                        break;
                    }
                    else if (i == 3 && item.Key == "MIN" && (item2.Id + item2.PoslednjeVreme.ToString().ToString()) == spojeno)
                    {
                        retVal = false;
                        break;
                    }
                }

            }

            return retVal;
        }

        public static List<PodaciIzBaze> SamoPoJednoPodrucje(List<PodaciIzBaze> listaDatuma)
        {
            List<PodaciIzBaze> retVal = listaDatuma;
            int i = 0;
            while (i < retVal.Count - 1)
            {
                if (retVal[i].ID == retVal[i + 1].ID)
                    retVal.RemoveAt(i);
                else
                    i++;
            }

            return retVal;
        }

        private static void DuplexSample(List<string> lista)
        {

            //"INSERT INTO UneseneVrednosti " +
            //"(IDGeoPodrucja, VremeMerenja, Vrednost) " +
            //"VALUES ('" + idGeoPodrucja + "', '" + datum + "', " + _unesenaPotrosnja + ")";

           // string s = "INSERT INTO UneseneVrednosti(IDGeoPodrucja, VremeMerenja, Vrednost) VALUES ('DO', '2018-6-1 00:30:00.000', 234)";
            var binding = new NetTcpBinding();
            var address = new EndpointAddress("net.tcp://localhost:10104/IInsertCulculationFunction");

            var clientCallback = new UpisiCallback2();
            var context = new InstanceContext(clientCallback);

            var factory = new DuplexChannelFactory<IInsertCulculationFunction>(clientCallback, binding, address);

            IInsertCulculationFunction messageChanel = factory.CreateChannel();

            Task.Run(() => messageChanel.PosaljiInsert(lista));
        }

        private static List<CalculationTable> IscitajIzMax()
        {
            string selectUpit = "SELECT * FROM FunkcijaMaximum";

            List<CalculationTable> podaci = new List<CalculationTable>();
            Polja.conn.Open();

            SqlCommand cmd = new SqlCommand(selectUpit, Polja.conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                podaci.Add(new CalculationTable((string)reader.GetSqlString(0), 
                                                reader.GetSqlDateTime(1), 
                                                (double)reader.GetSqlDouble(2), 
                                                reader.GetSqlDateTime(4)));
            }
            reader.Close();
            Polja.conn.Close();

            return podaci;
        }


    }
}
