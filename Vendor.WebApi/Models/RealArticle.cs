using System;
using Vendor.WebApi.Models.Data;

namespace Vendor.WebApi.Models
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