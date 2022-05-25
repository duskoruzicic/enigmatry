using System;
using System.Web.Http;
using Shop.WebApi.Models;
using Shop.WebApi.Repositories;
using Shop.WebApi.Services;

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
        public void BuyArticle(Article article, int buyerId)
        {
            var id = article.ID;
            try
            {
                if (article == null)
                {
                    throw new ArgumentNullException("Could not order article");
                }

                logger.Debug("Trying to sell article with id=" + id);

                article.IsSold = true;
                article.SoldDate = DateTime.Now;
                article.BuyerUserId = buyerId;

           
                dbRepository.Save(article);
                logger.Info("Article with id " + id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id " + id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}