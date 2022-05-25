using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vendor.WebApi.Repositories
{
    public static class RepositoryFactory
    {
        public static DbRepository CreateDbRepository()
        {
            return new DbRepository();
        }
    }
}