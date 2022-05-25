using Shared.Models;
using Shared.Models.Data;
using System;

namespace Vendor.WebApi.Services
{
    public class SupplierService : ISupplierService
    {
        private bool ArticleInInventory(int id)
        {
            return new Random().NextDouble() >= 0.5;
        }

        public Article GetArticle(int id)
        {
           ArticleData articleData = new ArticleData()
            {
                ID = id,
                Name = $"Article {id}",
                Price = new Random().Next(100, 500)
            };

            Article article = (Article)new RealArticle(articleData);

            return ArticleInInventory(id) ? article : new FakeArticle();
        }

        public Article MarkArticleAsSold(Article article, int buyerId)
        {

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerId = buyerId;

            return article;
        }
    }
}