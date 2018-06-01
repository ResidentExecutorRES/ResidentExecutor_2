using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IUpisiCallback1
    {
        [OperationContract(IsOneWay = true)]
        void OnCallback1(string message);
    }

    [ServiceContract]
    public interface IInsert
    {
        [OperationContract]
        List<string> InsertIntoTable(List<Dictionary<Tuple<int, string>, Tuple<DateTime, float>>> listaUbacenihVrednostiWhile);
    }
}
