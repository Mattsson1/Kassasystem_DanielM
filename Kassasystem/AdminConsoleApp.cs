namespace Kassasystem
{
    public partial class AdminConsoleApp
    {

        //Ändra
        //Lägga Till
        private int val;

        public void Admin()
        {
            Console.WriteLine("1. Lägg till produkt");
            Console.WriteLine("2. Ta bort produkt");
            Console.WriteLine("3. Ändra befintlig produkt ");
            
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

                }

            }
            else
            {
                Console.WriteLine("Ogiltig inmatning");
            }
        }
    }
}
