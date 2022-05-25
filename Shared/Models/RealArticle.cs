using System;
using Shared.Models;
using Shared.Models.Data;

namespace Shared.Models
{
    public class RealArticle : Article
    {
        public RealArticle(ArticleData articleData) 
        {
            this.Name = articleData.Name;
            this.Price = articleData.Price;
            this.IsSold = false;
        }
    }
}