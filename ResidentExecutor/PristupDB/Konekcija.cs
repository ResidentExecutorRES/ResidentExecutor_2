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
    
    public class Konekcija : IDanasnjiDatum
    {

        //public static string Unos = "";
        //Vraca redove iz tabele UneseneVrednosti gdje je datum jednak danasnjem datumu
        public  List<PodaciIzBaze> DanasnjiDatum()
        {
            List<PodaciIzBaze> lista = new List<PodaciIzBaze>();

            Polja.conn.Open();
            string upit = "SELECT * FROM UneseneVrednosti WHERE CONVERT(DATE, VremeMerenja) = CONVERT(DATE, GETDATE())";

            SqlCommand cmd = new SqlCommand(upit, Polja.conn);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                lista.Add(new PodaciIzBaze((string)reader.GetSqlString(0), reader.GetSqlDateTime(1), (float)reader.GetSqlDouble(2)));
            }

            reader.Close();
            Polja.conn.Close();

            return lista;
        }
    }
}
