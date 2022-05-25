using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TestBuilder
    {

        public Article MakeInputArticleWithIdOne()
        {
            return new FakeArticle()
            {
                Name = "Article 1"
            };
        }

        public Article MakeInputArticleWithIdZero()
        {
            return new FakeArticle()
            {
                Name = "Article 0"
            };
        }
    }
}
