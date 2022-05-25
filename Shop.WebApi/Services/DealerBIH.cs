using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Shared.Models;
using Shop.WebApi.Services.Interfaces;

namespace Shop.WebApi.Services
{
    public class DealerBIH : ArticleStorage, IDealerBIH
    {
        private readonly string _supplierUrl;
        private readonly ApiCaller _apicaller;

        public DealerBIH()
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

            return this.ArticleInInventory(id) && article.Price <= maxExpectedPrice ? article : null;

        }
    }
}