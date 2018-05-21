using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Contract
{
    [XmlRoot("RESIDENTNE_FUNKCIJE")]
    public class FUNKCIJA
    {
        public int ID { get; set; }
        public int UKLJUCENO { get; set; }
    }
}
