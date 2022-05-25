using System.Collections.Generic;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    public class CachedSupplier : ArticleStorage, IArticleStorage
    {
        private Dictionary<int, Article> _cachedArticles = new Dictionary<int, Article>();
        protected override bool ArticleInInventory(int id)
        {
            return _cachedArticles.ContainsKey(id);
        }

        public override Article GetArticle(int id, int maxExpectedPrice)
        {
            Article article;
            _cachedArticles.TryGetValue(id, out article);
            return this.ArticleInInventory(id) && article.Price <= maxExpectedPrice ? article :
                                                                        new Warehouse().GetArticle(id, maxExpectedPrice);
        }

        public void SetArticle(Article article)
        {
            _cachedArticles.Add(article.ID, article);
        }
    }
}