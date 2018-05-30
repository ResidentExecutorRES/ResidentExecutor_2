using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Contract
{
    public class RadSaXML
    {
        private string putanja = @"../../../rezidentne_funkcije.xml";
        public List<FUNKCIJA> CitajIzXML()
        {
            XmlSerializer desrializer = new XmlSerializer(typeof(List<FUNKCIJA>), new XmlRootAttribute("REZIDENTNE_FUNKCIJE"));
            List<FUNKCIJA> retVal = new List<FUNKCIJA>();
            /* XmlSerializer serializer = new XmlSerializer(typeof(List<FUNKCIJA>), new XmlRootAttribute("REZIDENTNE_FUNKCIJE"));
            using (TextWriter writer = new StreamWriter(putanja))
            {
                serializer.Serialize(writer, lista);
            }*/
            using (TextReader reader = new StreamReader(putanja))
            {
                object obj = desrializer.Deserialize(reader);
                retVal = (List<FUNKCIJA>)obj;
            }
            return retVal;
        }

        public List<GeoPodrucja> CitajIzXMLGeoPodrucja()
        {
            XmlSerializer desrializer = new XmlSerializer(typeof(List<GeoPodrucja>), new XmlRootAttribute("GEOGRAFSKA_PODRUCJA"));
            List<GeoPodrucja> retVal = new List<GeoPodrucja>();

            using (TextReader reader = new StreamReader("../../../geo_podrucja.xml"))
            {
                object obj = desrializer.Deserialize(reader);
                retVal = (List<GeoPodrucja>)obj;
            }
            return retVal;
        }


    }
}
