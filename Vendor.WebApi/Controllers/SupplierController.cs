using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vendor.WebApi.Models;
using Vendor.WebApi.Repositories;
using Vendor.WebApi.Services;
using Vendor.WebApi.Validators;

namespace Vendor.WebApi.Controllers
{
    public class SupplierController : ApiController
    {
        private Logger logger;


        private SupplierService _supplierService;
        private DbRepository _dbRepository;

        public SupplierController()
        {
            logger = Logger.Instance;
            _supplierService = new SupplierService();
            _dbRepository = RepositoryFactory.CreateDbRepository();
        }

        [HttpGet]
        [Route("api/getarticle")]
        public Article GetArticle(int id)
        {
            return _supplierService.GetArticle(id);
        }

        [HttpPost]
        [Route("api/buyarticle")]
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

                article.IsSold = true;
                article.SoldDate = DateTime.Now;
                article.BuyerId = buyerId;

                List<string> errormessages = article.Validate();

                if (errormessages.Any())
                {
                    logger.Error(string.Join(",", errormessages));
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }


                _dbRepository.Save(article);
                logger.Info("Article with id=" + id + " is sold.");
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id=" + id);
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