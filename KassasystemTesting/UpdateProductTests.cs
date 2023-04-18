
using Kassasystem.Models;

namespace KassasystemTesting
{
    [TestClass]
    public class UpdateProductTests
    {
        private readonly Kassasystem.ProductHelper sut;

        public UpdateProductTests()
        {
            sut = new Kassasystem.ProductHelper();
        }

        [TestMethod]
        public void Test_ConvertProductToListString()
        {
            // Arrange
            var productList = new List<Produkt> {
                new Produkt { ProductID = "1", ProductName = "Produkt1", BasePrice = "10", Unit = "kr/st" },
                new Produkt { ProductID = "2", ProductName = "Produkt2", BasePrice = "20", Unit = "kr/kg" }
            };
            
            // Act
            var result = sut.ConvertProductToListString(productList);

            // Assert
            Assert.AreEqual("1.Produkt1.10.kr/st", result[0]);
            Assert.AreEqual("2.Produkt2.20.kr/kg", result[1]);
        }


    }
}
