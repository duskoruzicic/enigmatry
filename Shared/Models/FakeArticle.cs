using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Models
{
    public class FakeArticle : Article
    {
        public FakeArticle() 
        {
            this.Name = "I am not existing!!!";
        }
    }
}