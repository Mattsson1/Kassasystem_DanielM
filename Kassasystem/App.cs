using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    internal class App
    {
        public void Run()
        {
            var nyKund = new Program.NyKund();
            

            Console.WriteLine("KASSA");
            Console.WriteLine("1. Ny kund");
            Console.WriteLine("2. Avsluta");

            int val;

            if(int.TryParse(Console.ReadLine(), out val))
            {
                switch (val)
                {
                    case 1:
                        nyKund.NewCustomer();

                        break;

                }



            }

    }
}
