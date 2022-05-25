using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.WebApi.Services.Interfaces
{
    public interface ICachedSupplier : IArticleStorage
    {
        void SetArticle(Article article);
    }
}
