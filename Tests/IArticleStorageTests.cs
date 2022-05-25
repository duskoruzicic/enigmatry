using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Shared.Models;
using Shop.WebApi;
using Shop.WebApi.Services;
using Shop.WebApi.Services.Interfaces;

namespace Tests
{
    [TestFixture]
    public abstract class IArticleStorageTests
    {
        private IArticleStorage articleStorage;

        private ApiCaller _apicaller;
        private Mock<IJsonSerializer> mockJsonSerializer;


        private string responseBody;

        [OneTimeSetUp]
        public void Init()
        {
            _apicaller = new ApiCaller();
            mockJsonSerializer = new Mock<IJsonSerializer>();
        }
        public abstract IArticleStorage CreateSut();

      
        [Test]
        public void GetArticleWithIdOne()
        {
            //Arange
            int id = 1;
            int maxExpectedPrice = 200;
            articleStorage = CreateSut();

            Article articleinput = new FakeArticle()
            {
                Name = "ArticleS 1"
            }; 
            responseBody = new JsonSerializer().Serialize<Article>(articleinput);
            mockJsonSerializer.Setup(m => m.Deserialize<Article>(It.IsAny<string>())).Returns(articleinput);

            //Act
            Article article = articleStorage.GetArticle(id, maxExpectedPrice);

            //Assert
            Assert.AreEqual("Article 1", article.Name);
        }

        [Test]
        public void GetArticleWithIdZero()
        {
            //Arange
            int id = 0;
            int maxExpectedPrice = 200;
            articleStorage = CreateSut();

            Article articleinput = new FakeArticle()
            {
                Name = "Article 0"
            };
            responseBody = new JsonSerializer().Serialize<Article>(articleinput);
            mockJsonSerializer.Setup(m => m.Deserialize<Article>(It.IsAny<string>())).Returns(articleinput);

            //Act
            Article article = articleStorage.GetArticle(id, maxExpectedPrice);

            //Assert
            Assert.AreEqual("Article 0", article.Name);
        }
    }
}
