using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.Models;
using Shared.Repositories;
using Shared.Services;
using Shared.Validators;
using Shop.WebApi.Services;
using Shop.WebApi.Services.Interfaces;


namespace Shop.WebApi.Controllers
{
    public class ShopController : ApiController
    {
        private Logger logger;
        private DbRepository dbRepository;
        private ICachedSupplier _cachedSupplier;



        public ShopController(ICachedSupplier cachedSupplier)
        {
            dbRepository = RepositoryFactory.CreateDbRepository();
            logger = Logger.Instance;
            _cachedSupplier = cachedSupplier;
        }

        [HttpGet]
        [Route("shop/getarticle")]
        public Article GetArtice(int id, int maxExpectedPrice = 200)
        {
            Article article = _cachedSupplier.GetArticle(id, maxExpectedPrice);

            if (article != null)
            {
                _cachedSupplier.SetArticle(article);
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

                _cachedSupplier.MarkArticleAsSold(article, buyerId);

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