using System;
using Vendor.WebApi.Models;

namespace Vendor.WebApi.Services
{
    public class SupplierService
    {
        private bool ArticleInInventory(int id)
        {
            return new Random().NextDouble() >= 0.5;
        }

        public Article GetArticle(int id)
        {
            Models.Data.ArticleData articleData = new Models.Data.ArticleData()
            {
                ID = id,
                Name = $"Article {id}",
                Price = new Random().Next(100, 500)
            };

            Article article = (Article)new RealArticle(articleData);

            return ArticleInInventory(id) ? article : new FakeArticle();
        }
    }
}