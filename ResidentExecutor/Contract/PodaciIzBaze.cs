using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class PodaciIzBaze
    {
        public PodaciIzBaze(string iD, SqlDateTime vreme, float vrednost)
        {
            ID = iD;
            Vreme = vreme;
            Vrednost = vrednost;
        }

        public string ID { get; set; }
        public SqlDateTime Vreme { get; set; }
        public float Vrednost { get; set; }

        //public override string ToString()
        //{
        //    return "ID: " + ID + " Vreme: " + Vreme + " Vrednost: " + Vrednost;
        //}


    }
}
