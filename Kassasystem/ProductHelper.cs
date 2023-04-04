namespace Kassasystem
{
    public class ProductHelper
    {
        private string produktPath = @".\Produkt.txt";
        public void PrintProducts()
        {
            List<Produkt> produkts = ReadProductFile();
            var sortedList = produkts.OrderBy(p => int.Parse(p.ProduktID)).ToList();
            foreach (var s in sortedList)
            {
                Console.Write("ID:");
                Console.Write($"{s.ProduktID}", Console.ForegroundColor = ConsoleColor.DarkRed);
                Console.Write($"-{s.ProduktNamn} {s.BasePrice}{s.Enhet}\n", Console.ForegroundColor = ConsoleColor.White);
            }
        }
        public List<Produkt> ReadProductFile()
        {
            

            if (!File.Exists(produktPath))
            {
                File.Create(produktPath);
            }
            List<string> lines = File.ReadAllLines(produktPath).ToList();

            var produkter = new List<Produkt>();

            foreach (string line in lines)
            {
                var readProdukt = new Produkt();

                string[] strings = line.Split(".");
                readProdukt.ProduktID = strings[0];
                readProdukt.ProduktNamn = strings[1];
                readProdukt.BasePrice = strings[2];
                readProdukt.Enhet = strings[3];

                produkter.Add(readProdukt);
            }
            return produkter;
        }

        public List<string> ConvertToListString(List<Produkt> products)
        {
            List<string> produktStrings = products.Select(s => $"{s.ProduktID}.{s.ProduktNamn}.{s.BasePrice}.{s.Enhet}").ToList();
            return produktStrings;
        }

    }
}
