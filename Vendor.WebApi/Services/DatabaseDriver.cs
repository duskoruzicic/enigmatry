using System.Collections.Generic;
using System.Linq;
using Vendor.WebApi.Models;

namespace Vendor.WebApi.Services
{
    public class DatabaseDriver
    {
        private List<RealArticle> _articles = new List<RealArticle>();

        public RealArticle GetById(int id)
        {
            return _articles.Single(x => x.ID == id);
        }

        public void Save(RealArticle article)
        {
            _articles.Add(article);
        }
    }
}