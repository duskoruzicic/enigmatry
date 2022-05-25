using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;
using Shop.WebApi.Services;
using Shop.WebApi.Validators;

namespace Shop.WebApi.Controllers
{
    public class ShopController : ApiController
    {
        private Logger logger;
        private DbRepository dbRepository;

        private CachedSupplier CachedSupplier;
        private Warehouse Warehouse;
        private Dealer1 Dealer1;
        private Dealer2 Dealer2;

        public ShopController()
        {
            dbRepository = RepositoryFactory.CreateDbRepository();
            logger = Logger.Instance;
            CachedSupplier = new CachedSupplier();
            Warehouse = new Warehouse();
            Dealer1 = new Dealer1();
            Dealer2 = new Dealer2();
        }

        [HttpGet]
        [Route("shop/getarticle")]
        public Article GetArtice(int id, int maxExpectedPrice = 200)
        {
            Article article = CachedSupplier.GetArticle(id, maxExpectedPrice);

            if (article != null)
            {
                CachedSupplier.SetArticle(article);
            }

            return article;
        }

        [HttpPost]
        [Route("shop/buyarticle")]
        public HttpResponseMessage BuyArticle(Article article, int buyerId)
        {
            var id = article.ID;
            try
            {
                if (article == null)
                {
                    throw new ArgumentNullException("Could not order article");
                }

                logger.Debug("Trying to sell article with id=" + id);

                CachedSupplier.MarkArticleAsSold(article, buyerId);

                List<string> errormessages = article.Validate();

                if (errormessages.Any())
                {
                    logger.Error(string.Join(",", errormessages));
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }

                dbRepository.Save(article);
                logger.Info("Article with id " + id + " is sold.");

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id " + id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}