using System;
using Shared.Models;
using Shared.Models.Data;
using Shop.WebApi.Services.Interfaces;

namespace Shop.WebApi.Services
{
    public class Warehouse : ArticleStorage, IWarehouse
    {
        protected override bool ArticleInInventory(int id)
        {
            return new Random().NextDouble() >= 0.5;
        }

        public override Article GetArticle(int id, int maxExpectedPrice)
        {
            ArticleData articleData = new ArticleData()
            {
                ID = id,
                Name = $"Article {id}",
                Price = new Random().Next(100, 500)
            };

            RealArticle article = new RealArticle(articleData);

            return this.ArticleInInventory(id) && article.Price <= maxExpectedPrice ? article :
                                                                new DealerRS().GetArticle(id, maxExpectedPrice);
        }
    }
}