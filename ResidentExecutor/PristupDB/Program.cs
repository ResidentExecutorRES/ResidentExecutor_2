using Contract;
using KorisnickiInterfejs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PristupDB
{
    public class Polja
    {
        public static SqlConnection conn = new SqlConnection(@"Server=DESKTOP-JJ3CM3A;  Database=ResidentExecutor_DB;  Integrated Security=True");
        public static string[] podaciIzFajla = File.ReadAllLines(@"../../../PodaciSaMainWindow.txt");
        public static SqlCommand cmd = null;                
    }

    class Program
    {
        public static ServiceHost svc = null;
        public static ServiceHost svcInsertInto = null;
        public static ServiceHost svcUpisi = null;

        public static ServiceHost svcDanasnjiDatum = null;
        public static ServiceHost svcInsertList = null;
        public static ServiceHost svcProveriPreInsert = null;

        //public static Service

        static void Main(string[] args)
        {
            Open();

            while (true)
            {            
                Thread.Sleep(1000);
                //Console.ReadKey();
            }
        }

        public static void Open()
        {
            svc = new ServiceHost(typeof(Connect));
           // svcInsertInto = new ServiceHost(typeof(InsertInto));
            svcUpisi = new ServiceHost(typeof(Upisi));
            svcDanasnjiDatum = new ServiceHost(typeof(Konekcija));

            svcInsertList = new ServiceHost(typeof(InsertCaluculationFunction));
            svcProveriPreInsert = new ServiceHost(typeof(ProveriPreInsert));

            svc.AddServiceEndpoint(typeof(IConnect), new NetTcpBinding(), "net.tcp://localhost:10100/IConnect");
            //svcInsertInto.AddServiceEndpoint(typeof(IInsert), new NetTcpBinding(), "net.tcp://localhost:10101/IInsert");
            
            svcDanasnjiDatum.AddServiceEndpoint(typeof(IDanasnjiDatum), new NetTcpBinding(), "net.tcp://localhost:10103/IDanasnjiDatum");

            svcUpisi.AddServiceEndpoint(typeof(IUpisi), new NetTcpBinding(), "net.tcp://localhost:10102/IUpisi");
            svcInsertList.AddServiceEndpoint(typeof(IInsertCulculationFunction), new NetTcpBinding(), "net.tcp://localhost:10104/IInsertCulculationFunction");
            svcProveriPreInsert.AddServiceEndpoint(typeof(IProveriPreInsert), new NetTcpBinding(), "net.tcp://localhost:10105/IProveriPreInsert");

            svc.Open();
           // svcInsertInto.Open();
            svcUpisi.Open();
            svcDanasnjiDatum.Open();
            svcInsertList.Open();
            svcProveriPreInsert.Open();

            Console.WriteLine("Server je otvoren");
        }

        public static void Close()
        {
            svc.Close();
            //svcInsertInto.Close();
            svcUpisi.Close();
            svcDanasnjiDatum.Close();
            svcInsertList.Close();
            svcProveriPreInsert.Close();

            Console.WriteLine("Server je zatvoren");
        }

       

        public static void IzvrsiInsert(string s)
        {         
            using (Polja.cmd = new SqlCommand(s, Polja.conn))
            {
                Polja.conn.Open();
                Polja.cmd.ExecuteNonQuery();
                Polja.conn.Close();
            }
        }

        //public static  List<PodaciIzBaze> DanasnjiDatum()
        //{
        //    List<PodaciIzBaze> lista = new List<PodaciIzBaze>();


        //    conn.Open();
        //    //string upit = "SELECT * FROM UneseneVrednosti WHERE CONVERT(DATE, VremeMerenja) = CONVERT(DATE, GETDATE())";

        //    SqlCommand cmd = new SqlCommand(MainWindow.Upit, conn);
        //    SqlDataReader reader = cmd.ExecuteReader();


        //    while(reader.Read())
        //    {
        //        lista.Add(new PodaciIzBaze((string)reader.GetSqlString(0), reader.GetSqlDateTime(1), (float)reader.GetSqlDouble(2)));
        //    }

        //    reader.Close();
        //    conn.Close();

        //    return lista;
        //}
        //Vraca redove iz tabele UneseneVrednosti
        //Ako u ovoj listi postoji neka vrednost INSERT iz MainWindow.Upit se ne sme izvrsiti
        //public static List<PodaciIzBaze> IzvrsiSelect()
        //{
        //    List<PodaciIzBaze> podaci = new List<PodaciIzBaze>();

        //    do
        //    {


        //    }
        //    while (MainWindow.SelectUpit == "");

        //    conn.Open();

        //    SqlCommand cmd = new SqlCommand(MainWindow.SelectUpit, conn);

        //    SqlDataReader reader = cmd.ExecuteReader();

        //    while(reader.Read())
        //    {
        //        podaci.Add(new PodaciIzBaze((string)reader.GetSqlString(0), reader.GetSqlDateTime(1), (float)reader.GetSqlDouble(2)));
        //    }
        //    reader.Close();
        //    conn.Close();

        //    return podaci;

        //}

        //public static bool ProveriDatum()
        //{

        //    List<PodaciIzBaze> lista = IzvrsiSelect();
        //    foreach (var item in lista)
        //    {
        //        if (item.Vreme == GetDatum())
        //            return false;
        //    }
        //    return true;

        //}

        //public static SqlDateTime GetDatum()
        //{
        //    string[] splitovano = podaciIzFajla[0].Split('\'');
        //    return SqlDateTime.Parse(splitovano[3]);
        //}
    }
}
