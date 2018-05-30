using Contract;
using KorisnickiInterfejs;
using PristupDB;
using ServisniSloj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatskoUpravljanje
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ResidentE.listaUbacenihVrednostiWhile.Add(ResidentE.Automatic());
                Thread.Sleep(10000);
                if (Konekcija.Unos == "exit")
                    return;
            }

            

        }
    }
}
