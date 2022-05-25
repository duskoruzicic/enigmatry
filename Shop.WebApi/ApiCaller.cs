using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Shop.WebApi
{
    public class ApiCaller
    {
        public Task<HttpResponseMessage> SendRequest(string url, int id)
        {
            using (var client = new HttpClient())
            {
                return client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{url}/ArticleInInventory/{id}"));
            }
        }

    }
}