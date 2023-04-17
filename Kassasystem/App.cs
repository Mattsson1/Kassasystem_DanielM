namespace Kassasystem
{
    public class App
    {
        private int val;
        private bool isSelectionOk = false;
        public void Run()
        {           
            Console.Clear();
            var nyKund = new CashRegister();
            var adminApp = new AdminConsoleApp();                    
            
            while (isSelectionOk == false)
            {
                Console.Clear();
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("2. Avsluta");
                Console.WriteLine("3. Admin");

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
                        case 3:
                            adminApp.Admin();
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning");
                    Console.ReadKey();
                }
            }
        }
    }
}
