namespace Kassasystem
{
    public class ProductHelper
    {
        public void PrintProducts()
        {
            List<Produkt> produkts = ReadProductFile();
            var sortedList = produkts.OrderBy(p => int.Parse(p.ProduktID)).ToList();
            foreach (var s in sortedList)
            {
                Console.Write("ID:");
                Console.Write($"{s.ProduktID}", Console.ForegroundColor = ConsoleColor.DarkRed);
                Console.Write($"-{s.ProduktNamn} {s.Pris}{s.Enhet}\n", Console.ForegroundColor = ConsoleColor.White);
            }
        }
        public List<Produkt> ReadProductFile()
        {
            string produktPath = @".\Produkt.txt";

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
                readProdukt.Pris = strings[2];
                readProdukt.Enhet = strings[3];

                produkter.Add(readProdukt);
            }
            return produkter;
        }

        public List<string> ConvertToListString(List<Produkt> products)
        {
            List<string> produktStrings = products.Select(s => $"{s.ProduktID}.{s.ProduktNamn}.{s.Pris}.{s.Enhet}").ToList();
            return produktStrings;
        }

    }
}
