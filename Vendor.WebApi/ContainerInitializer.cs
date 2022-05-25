using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Vendor.WebApi.Services;

namespace Vendor.WebApi
{
    public class ContainerInitializer
    {
        private static ContainerBuilder objContainer { get; set; }
        public static Autofac.IContainer Container { get; set; }

        public static IContainer GetContainer()
        {
            objContainer = new ContainerBuilder();

            objContainer.RegisterType<SupplierService>()
                           .As<ISupplierService>();


            objContainer.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            Container = objContainer.Build();

            return Container;
        }
    }
}