using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IUpisiCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnCallback(string message);
    }

    [ServiceContract(CallbackContract = typeof(IUpisiCallback))]
    public interface IUpisi
    {
        [OperationContract]
        void PosaljiInsert(string insertInto);
    }
}
