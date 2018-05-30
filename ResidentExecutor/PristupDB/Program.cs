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


        static void Main(string[] args)
        {
            

            Thread.Sleep(5000);

            Open();

            while (true)
            {
                if (Polja.podaciIzFajla[0] != "")
                {
                    IzvrsiInsert();
                    break;
                }
            }

            Console.ReadKey();

            Close();
                      
        }

        public static void Open()
        {
            svc = new ServiceHost(typeof(Connect));
            svc.AddServiceEndpoint(typeof(IConnect), new NetTcpBinding(), "net.tcp://localhost:10100/IConnect");
            svc.Open();

            

            Console.WriteLine("Server je otvoren");
        }

        public static void Close()
        {
            svc.Close();
            Console.WriteLine("Server je zatvoren");
        }

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

        public static void IzvrsiInsert()
        {         
            using (Polja.cmd = new SqlCommand(Polja.podaciIzFajla[0], Polja.conn))
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
    }
}
