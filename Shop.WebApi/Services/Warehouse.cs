﻿using System;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    public class Warehouse : ArticleStorage, IArticleStorage
    {
        protected override bool ArticleInInventory(int id)
        {
            return new Random().NextDouble() >= 0.5;
        }

        public override Article GetArticle(int id, int maxExpectedPrice)
        {
            Article article = new Article()
            {
                ID = id,
                Name = $"Article {id}",
                Price = new Random().Next(100, 500)
            };

            return this.ArticleInInventory(id) && article.Price <= maxExpectedPrice ? article :
                                                                new Dealer1().GetArticle(id, maxExpectedPrice);
        }
    }
}