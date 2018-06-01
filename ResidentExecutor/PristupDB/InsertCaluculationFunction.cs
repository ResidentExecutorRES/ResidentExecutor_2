using Contract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristupDB
{
    public class InsertCaluculationFunction : IInsertCulculationFunction
    {
       
        public SqlConnection conn = new SqlConnection(@"Server=DESKTOP-JJ3CM3A;  Database=ResidentExecutor_DB;  Integrated Security=True");
        //public static string[] podaciIzFajla = File.ReadAllLines(@"../../../PodaciSaMainWindow.txt");
        public SqlCommand cmd = null;
        public void PosaljiInsert(List<string> insertInto)
        {
            foreach (var item in insertInto)
            {

                using (cmd = new SqlCommand(item, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private async void TaskCallback(object callback)
        {
            IUpisiCallback2 messageCallback = callback as IUpisiCallback2;

            for (int i = 0; i < 10; i++)
            {
                messageCallback.OnCallback("message " + i.ToString());
                await Task.Delay(1000);
            }
        }

    }
}
