namespace Kassasystem
{
    public partial class AdminConsoleApp
    {
        private int val;

        public void Admin()
        {
            Console.WriteLine("1. Lägg till produkt");
            Console.WriteLine("2. Ta bort produkt");
            Console.WriteLine("3. Ändra befintlig produkt ");
            Console.WriteLine("4. Adminstera kampanjer");
            Console.WriteLine("5. Tillbaka till meny");

            if (int.TryParse(Console.ReadLine(), out val))
            {
                switch (val)
                {
                    case 1:
                        var addProduct = new AddProduct();
                        addProduct.AdminCase1Add();
                        break;
                    case 2:
                        var deleteProduct = new DeleteProduct();
                        deleteProduct.Delete();
                        break;
                    case 3:
                        var updateProduct = new UpdateProduct();
                        updateProduct.Update();
                        break;
                    case 4:
                        var campaignManager = new CampaignManager();
                        campaignManager.CampaignSelect();
                        break;
                    case 5:
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
