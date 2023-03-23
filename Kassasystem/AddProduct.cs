namespace Kassasystem
{
    public class AddProduct
    {
        private string ProduktID, ProduktNamn, Pris, Enhet;
        private int lineCounter = 0;
        private int val;

        public void AdminCase1Add()
        {
            string produktPath = @".\Produkt.txt";
            var nyKund = new NyKund();

            List<Produkt> produkts = nyKund.ReadProductFile();
            List<string> produktAdds = new List<string>();

            Console.WriteLine("Ange namnet på produkten");
            ProduktNamn = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Vilken Pris enhet vill du ha på produkten?");
            Console.WriteLine("1. KR/ST");
            Console.WriteLine("2. KR/KG");

            if (int.TryParse(Console.ReadLine(), out val))
            {
                if (val == 1) Enhet = "kr/st";
                if (val == 2) Enhet = "kr/kg";
            }
            else
            {
                Console.WriteLine("Välj mellan de två alternativen");
            }
 
            Console.WriteLine("Ange hur mycket produkten ska kosta");
            Pris = Console.ReadLine();


            foreach (var s in produkts)
            {
                lineCounter++;
            }
            lineCounter++;
            var counterString = Convert.ToString(lineCounter);

            produktAdds.Add($"{counterString}.{ProduktNamn}. {Pris}. {Enhet}");

            File.AppendAllLines(produktPath, produktAdds);

            //List<ProduktAdd> addProdukt = new List<ProduktAdd>();


            Console.ReadKey();

        }
    }
}