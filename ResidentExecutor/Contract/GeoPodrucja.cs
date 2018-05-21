using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class GeoPodrucja
    {
        public static readonly Dictionary<string, string> geoPodrucja = new Dictionary<string, string>()
        {
            { "BL", "Banjaluka"},
            { "BN", "Bijeljina"},
            { "DO", "Doboj"},
            { "PR", "Prnjavor"},
            { "SA", "Sarajevo"},
            { "TR", "Trebinje"},
            { "ZV", "Zvornik"},
            { "GR", "Gradiska"},
            { "BI", "Bihac"},
            { "MO", "Mostar" }
        };
    }
}
