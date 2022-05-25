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
            return true;
        }

        public override Article GetArticle(int id, int maxExpectedPrice)
        {
            ArticleData articleData = new ArticleData()
            {
                ID = id,
                Name = $"Article {id}",
                Price = new Random().Next(50, 190)
            };

            RealArticle article = new RealArticle(articleData);

            return this.ArticleInInventory(id) && article.Price <= maxExpectedPrice ? article :
                                                                new Dealer().GetArticle(id, maxExpectedPrice);
        }
    }
}