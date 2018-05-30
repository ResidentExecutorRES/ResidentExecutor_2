using Contract;
using KorisnickiInterfejs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupDB
{
    public class Connect : IConnect
    {
        public List<PodaciIzBaze> VratiRedove(string selectUpit)
        {
            List<PodaciIzBaze> podaci = new List<PodaciIzBaze>();           

            Polja.conn.Open();

            SqlCommand cmd = new SqlCommand(selectUpit, Polja.conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                podaci.Add(new PodaciIzBaze((string)reader.GetSqlString(0), reader.GetSqlDateTime(1), (float)reader.GetSqlDouble(2)));
            }
            reader.Close();
            Polja.conn.Close();

            return podaci;

            //throw new NotImplementedException();
        }
    }
}
