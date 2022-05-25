using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebApi.Services
{
    public interface IArticleStorage
    {
        Article GetArticle(int id, int maxExpectedPrice);
    }
}