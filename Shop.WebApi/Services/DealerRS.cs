using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Shared.Models;
using Shop.WebApi.Services.Interfaces;

namespace Shop.WebApi.Services
{
    public class DealerRS : ArticleStorage, IDealerRS
    {
        private readonly string _supplierUrl;
        private readonly ApiCaller _apicaller;
        private readonly JsonSerializer _jsonSerializer;


        public DealerRS()
        {
            _supplierUrl = ConfigurationManager.AppSettings["Dealer2Url"];
            _apicaller = new ApiCaller();
            _jsonSerializer = new JsonSerializer();
        }

        protected override bool ArticleInInventory(int id)
        {
            var response = _apicaller.SendRequest(_supplierUrl, id);
            var hasArticle = _jsonSerializer.Deserialize<bool>(response.Result.Content.ReadAsStringAsync().Result);

            return hasArticle;

        }

        public override Article GetArticle(int id, int maxExpectedPrice)
        {

            var response = _apicaller.SendRequest(_supplierUrl, id);
            var article = _jsonSerializer.Deserialize<Article>(response.Result.Content.ReadAsStringAsync().Result);

            return this.ArticleInInventory(id) && article.Price <= maxExpectedPrice ? article : new DealerBIH().GetArticle(id, maxExpectedPrice);

        }
    }
}