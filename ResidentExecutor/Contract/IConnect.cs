using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [ServiceContract]
    public interface IConnect
    {
        [OperationContract]
        List<PodaciIzBaze> VratiRedove(string selectUpit);
    }
}
