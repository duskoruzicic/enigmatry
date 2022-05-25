using System;
using System.Web.Http;
using Shop.WebApi.Models;
using Shop.WebApi.Services;

namespace Shop.WebApi.Controllers
{
    public class ShopController : ApiController
    {
        private Db Db;
        private Logger logger;

        private CachedSupplier CachedSupplier;
        private Warehouse Warehouse;
        private Dealer1 Dealer1;
        private Dealer2 Dealer2;

        public ShopController()
        {
            Db = new Db();
            logger = new Logger();
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
        public void BuyArticle(Article article, int buyerId)
        {
            var id = article.ID;
            if (article == null)
            {
                throw new Exception("Could not order article");
            }

            logger.Debug("Trying to sell article with id=" + id);

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;

            try
            {
                Db.Save(article);
                logger.Info("Article with id " + id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id " + id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception)
            {
            }
        }
    }
}