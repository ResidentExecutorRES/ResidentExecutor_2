using Contract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupDB
{
    class Program
    {
        public static SqlConnection conn = new SqlConnection(@"Server=DESKTOP-JJ3CM3A;  Database=ResidentExecutor_DB;  Integrated Security=True");
        public static string[] podaciIzFajla = File.ReadAllLines(@"../../../PodaciSaMainWindow.txt");
        public static SqlCommand cmd = null;
        static void Main(string[] args)
        {

            if (ProveriDatum())
                IzvrsiInsert();

            Console.ReadKey();

        }
        
        public static List<PodaciIzBaze> IzvrsiSelect()
        {
            List<PodaciIzBaze> podaci = new List<PodaciIzBaze>();
            conn.Open();

            string upit = "SELECT * FROM UneseneVrednosti";
            SqlCommand cmd = new SqlCommand(upit, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                podaci.Add(new PodaciIzBaze((string)reader.GetSqlString(0), reader.GetSqlDateTime(1), (float)reader.GetSqlDouble(2)));
            }
            reader.Close();
            conn.Close();

            return podaci;

        }

        public static bool ProveriDatum()
        {
            
            List<PodaciIzBaze> lista = IzvrsiSelect();
            foreach (var item in lista)
            {
                if (item.Vreme == GetDatum())
                    return false;
            }
            return true;
            
        }

        public static SqlDateTime GetDatum()
        {
            string[] splitovano = podaciIzFajla[0].Split('\'');
            return SqlDateTime.Parse(splitovano[3]);
        }

        public static void IzvrsiInsert()
        {
            
            using (cmd = new SqlCommand(podaciIzFajla[0], conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
