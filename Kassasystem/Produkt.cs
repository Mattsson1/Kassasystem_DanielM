using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    internal class Produkt
    {

        public DateTime now = DateTime.Now;
        public string ProduktID { get; set; }
        public string ProduktNamn { get; set; }
        public string Pris { get; set; }
        public string Enhet { get; set; }
        public int lopNummer { get; set; }

        public List<Produkt> produkter = new List<Produkt>();

    }
}
