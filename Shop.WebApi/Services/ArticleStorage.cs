using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebApi.Services
{
    public abstract class ArticleStorage : IArticleStorage
    {
        protected abstract bool ArticleInInventory(int id);
        public abstract Article GetArticle(int id, int maxExpectedPrice);
    }
}