using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vendor.WebApi.Services;
using Shared.Repositories;
using Shared.Models;
using Shared.Validators;
using Shared.Services;

namespace Vendor.WebApi.Controllers
{
    public class SupplierController : ApiController
    {
        private Logger logger;


        private ISupplierService _supplierService;
        private DbRepository _dbRepository;

        public SupplierController(ISupplierService supplierService)
        {
            logger = Logger.Instance;
            _supplierService = supplierService;
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

                _supplierService.MarkArticleAsSold(article, buyerId);

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