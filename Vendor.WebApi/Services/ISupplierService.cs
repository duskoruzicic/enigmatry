using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendor.WebApi.Services
{
    public interface ISupplierService
    {
        Article GetArticle(int id);
        Article MarkArticleAsSold(Article article, int buyerId);
    }
}
