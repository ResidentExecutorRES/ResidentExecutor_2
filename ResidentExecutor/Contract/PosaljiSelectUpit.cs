using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [DataContract]
    public class PosaljiSelectUpit
    {        
        [DataMember]
        public  string Upit { get; set; }
        [DataMember]
        public  string SelectUpit { get; set; }
    }
}
