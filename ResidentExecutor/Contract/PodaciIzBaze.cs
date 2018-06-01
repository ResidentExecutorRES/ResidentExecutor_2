using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [DataContract]
    public class PodaciIzBaze
    {
        public PodaciIzBaze(string iD, SqlDateTime vreme, float vrednost)
        {
            ID = iD;
            Vreme = vreme;
            Vrednost = vrednost;
        }

        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public SqlDateTime Vreme { get; set; }
        [DataMember]
        public float Vrednost { get; set; }

        //public override string ToString()
        //{
        //    return "ID: " + ID + " Vreme: " + Vreme + " Vrednost: " + Vrednost;
        //}


    }
}
