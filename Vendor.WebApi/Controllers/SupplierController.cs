using System;
using System.Web.Http;
using Vendor.WebApi.Models;
using Vendor.WebApi.Repositories;
using Vendor.WebApi.Services;

namespace Vendor.WebApi.Controllers
{
    public class SupplierController : ApiController
    {

        private Logger logger;
        private DbRepository _dbRepository;

        private SupplierService _supplierService;

        public SupplierController()
        {
            _dbRepository = RepositoryFactory.CreateDbRepository();
            logger = new Logger();
            _supplierService = new SupplierService();
        }

        public Article GetArtice(int id)
        {
            return _supplierService.GetArticle(id);
        }

        public void BuyArticle(RealArticle article, int buyerId)
        {
            var id = article.ID;
            if (article == null)
            {
                throw new Exception("Could not order article");
            }

            logger.Debug("Trying to sell article with id=" + id);

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerId = buyerId;

            try
            {
                _dbRepository.Save(article);
                logger.Info("Article with id=" + id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id=" + id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception)
            {
            }
        }
    }
}