using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatskoUpravljanje
{
    public class UpisiCallback2 : IUpisiCallback2
    {
        public void OnCallback(string message)
        {
            Console.WriteLine("Message from server, {0}.", message);
        }
    }
}
