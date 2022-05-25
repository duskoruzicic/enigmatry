using Shop.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebApi.Validators
{
    public static class ArticleValidator
    {
        public static List<string> Validate(this Article article)
        {
            List<string> errorMessages = new List<string>();

            errorMessages.Add(ValidateName(article));
            errorMessages.Add(ValidatePrice(article));
            errorMessages.Add(ValidateSoldDate(article));
            errorMessages.Add(ValidateBuyerId(article));

            return errorMessages;
        }


        private static string ValidateName(Article article)
        {
            return string.IsNullOrEmpty(article.Name) ? "Article name is mandatory." : string.Empty;
        }

        private static string ValidatePrice(Article article)
        {
            return article.Price <= 0 ? "Article price must be greater than zero." : string.Empty;
        }

        private static string ValidateSoldDate(Article article)
        {
            return article.SoldDate > DateTime.Now ? "Article sold date cant be in the future." : string.Empty;
        }

        private static string ValidateBuyerId(Article article)
        {
            return article.BuyerId <= 0 ? "BuyerId must be greater than zero." : string.Empty;
        }
    }
}