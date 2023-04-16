
namespace Kassasystem.Models
{
    public class Produkt
    {

        public DateTime now = DateTime.Now;
        public string ProduktID { get; set; }
        public string ProduktNamn { get; set; }
        public string BasePrice { get; set; }
        public string Enhet { get; set; }
        public int lopNummer { get; set; }
        //public List<Campaign> campaigns { get; set; } 

        //public List<Produkt> produkter = new List<Produkt>();
        /*override public string ToString()
        {
            return $"{ProduktID},{}";
        }
        */
    }

    /*
    public class ProduktAdd
    {
        public string ProduktID { get; set; }
        public string ProduktNamn { get; set; }
        public string Pris { get; set; }
        public string Enhet { get; set; }

       

        public ProduktAdd(string produktId, string produktNamn, string pris, string enhet)
        {
            ProduktID = produktId;
            ProduktNamn = produktNamn;
            Pris = pris;
            Enhet = enhet;
        }

        public List<ProduktAdd> produkter = new List<ProduktAdd>();


    }
    */
}
