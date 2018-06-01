using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    
    public interface IUpisiCallback2
    {
        [OperationContract(IsOneWay = true)]
        void OnCallback(string message);
    }

    [ServiceContract(CallbackContract = typeof(IUpisiCallback2))]
    public interface IInsertCulculationFunction
    {
        [OperationContract]
        void PosaljiInsert(List<string> insertInto);
    }
}
