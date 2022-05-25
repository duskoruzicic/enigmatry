using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Models
{
    public abstract class Article
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        public bool IsSold { get; set; }

        public DateTime SoldDate { get; set; }
        public int BuyerId { get; set; }
    }
}