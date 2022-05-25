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
    public class DealerTests : IArticleStorageTests
    {
        public override IArticleStorage CreateSut()
        {
            Mock<IJsonSerializer> mockJsonSerializer = new Mock<IJsonSerializer>();
            return new Dealer( mockJsonSerializer.Object);
        }
    }
}
