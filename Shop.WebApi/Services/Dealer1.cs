using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Shop.WebApi.Models;

namespace Shop.WebApi.Services
{
    public class Dealer1 : ArticleStorage, IArticleStorage
    {
        private readonly string _supplierUrl;
        private readonly ApiCaller _apicaller;

        public Dealer1()
        {
            _supplierUrl = ConfigurationManager.AppSettings["Dealer2Url"];
            _apicaller = new ApiCaller();
        }

        protected override bool ArticleInInventory(int id)
        {
            var response = _apicaller.SendRequest(_supplierUrl, id);
            var hasArticle = JsonConvert.DeserializeObject<bool>(response.Result.Content.ReadAsStringAsync().Result);

            return hasArticle;

        }

        public override Article GetArticle(int id, int maxExpectedPrice)
        {

            var response = _apicaller.SendRequest(_supplierUrl, id);
            var article = JsonConvert.DeserializeObject<Article>(response.Result.Content.ReadAsStringAsync().Result);

            return this.ArticleInInventory(id) && article.ArticlePrice <= maxExpectedPrice ? article : new Dealer2().GetArticle(id, maxExpectedPrice);

        }
    }
}