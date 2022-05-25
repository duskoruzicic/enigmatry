using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Shared.Models;
using Shop.WebApi.Services.Interfaces;

namespace Shop.WebApi.Services
{
    public class Dealer : ArticleStorage, IDealer
    {
        private string _supplierUrl;
        private  ApiCaller _apicaller;
        private IJsonSerializer _jsonSerializer;

        public Dealer()
        {
            _supplierUrl = ConfigurationManager.AppSettings["Dealer"];
            _apicaller = new ApiCaller();
            _jsonSerializer = new JsonSerializer();
        }

        public Dealer(IJsonSerializer jsonSerializer)
        {
            _supplierUrl = ConfigurationManager.AppSettings["Dealer"];
            _apicaller = new ApiCaller();
            _jsonSerializer = jsonSerializer;
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