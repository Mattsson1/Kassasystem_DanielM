namespace Kassasystem
{
    internal class App
    {
        public void Run()
        {
            var nyKund = new Program.NyKund();
            int val;
            bool isSelectionOk = false;

            while (isSelectionOk == false)
            {
                Console.Clear();
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("2. Avsluta");


                if (int.TryParse(Console.ReadLine(), out val))
                {
                    switch (val)
                    {
                        case 1:
                            nyKund.NewCustomer();
                            break;
                        case 2:
                            isSelectionOk = true;
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning");
                }
            }
        }
    }
}
