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
    public class CalculationTable
    {
        public CalculationTable(string id, SqlDateTime vremeProracuna, double vrednost, SqlDateTime poslednjeVreme)
        {
            Id = id;
            VremeProracuna = vremeProracuna;
            Vrednost = vrednost;
            PoslednjeVreme = poslednjeVreme;
        }

        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public SqlDateTime VremeProracuna { get; set; }
        [DataMember]
        public double Vrednost { get; set; }
        [DataMember]
        public SqlDateTime PoslednjeVreme { get; set; }

        public override string ToString()
        {
            return "ID: " + Id + " VremeProracuna: " + VremeProracuna + " Vrednost: " + Vrednost + " PoslednjeVreme: " + PoslednjeVreme;
        }
    }
}
