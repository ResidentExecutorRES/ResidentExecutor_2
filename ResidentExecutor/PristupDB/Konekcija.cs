using Contract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupDB
{
    public class Konekcija
    {
        public static SqlConnection conn = new SqlConnection(@"Server=DESKTOP-JJ3CM3A;  Database=ResidentExecutor_DB;  Integrated Security=True");
        public static string[] podaciIzFajla = File.ReadAllLines(@"../../../PodaciSaMainWindow.txt");
        public static SqlCommand cmd = null;

        public static string Unos = "";

        public  List<PodaciIzBaze> DanasnjiDatum()
        {
            List<PodaciIzBaze> lista = new List<PodaciIzBaze>();

            conn.Open();
            string upit = "SELECT * FROM UneseneVrednosti WHERE CONVERT(DATE, VremeMerenja) = CONVERT(DATE, GETDATE())";

            SqlCommand cmd = new SqlCommand(upit, conn);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                lista.Add(new PodaciIzBaze((string)reader.GetSqlString(0), reader.GetSqlDateTime(1), (float)reader.GetSqlDouble(2)));
            }

            reader.Close();
            conn.Close();

            return lista;
        }
    }
}
