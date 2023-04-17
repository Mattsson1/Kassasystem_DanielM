
namespace Kassasystem.Models
{
    public class Produkt
    {

        public DateTime now = DateTime.Now;
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string BasePrice { get; set; }
        public string Unit { get; set; }
        public int SerialNumber { get; set; }
     
    }
   
}
