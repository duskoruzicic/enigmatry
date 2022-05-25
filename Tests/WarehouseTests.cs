using NUnit.Framework;
using Shop.WebApi.Services;
using Shop.WebApi.Services.Interfaces;
using Shared.Models;

namespace Tests
{
    [TestFixture]
    public class WarehouseTests : IArticleStorageTests
    {
        public override IArticleStorage CreateSut()
        {
            return new Warehouse();
        }
    }
}
