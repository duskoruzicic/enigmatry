using NUnit.Framework;
using Shop.WebApi.Services;
using Shop.WebApi.Services.Interfaces;
using Shared.Models;

namespace Tests
{
    [TestFixture]
    public class WarehouseTests
    {
        private IWarehouse warehouse;

        [Test]
        public void GetArticleWithIdOne()
        {
            //Arange
            int id = 1;
            int maxExpectedPrice = 200;
            warehouse = new Warehouse();

            //Act
            Article article = warehouse.GetArticle(id, maxExpectedPrice);

            //Assert
            Assert.AreEqual("Article 1", article.Name);
        }

        [Test]
        public void GetArticleWithIdZero()
        {
            //Arange
            int id = 0;
            int maxExpectedPrice = 200;
            warehouse = new Warehouse();

            //Act
            Article article = warehouse.GetArticle(id, maxExpectedPrice);

            //Assert
            Assert.AreEqual("Article 0", article.Name);
        }
    }
}
