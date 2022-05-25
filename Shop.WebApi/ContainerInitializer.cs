using Autofac;
using Autofac.Integration.WebApi;
using Shop.WebApi.Services;
using Shop.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Shop.WebApi
{
    public static class ContainerInitializer
    {
        private static ContainerBuilder objContainer { get; set; }
        public static Autofac.IContainer Container { get; set; }

        public static IContainer GetContainer()
        {
            objContainer = new ContainerBuilder();

            objContainer.RegisterType<CachedSupplier>()
                           .As<ICachedSupplier>();
            objContainer.RegisterType<Warehouse>()
                           .As<IWarehouse>();
            objContainer.RegisterType<Dealer>()
                           .As<IDealer>();
            objContainer.RegisterType<JsonSerializer>()
                          .As<IJsonSerializer>();


            objContainer.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            Container = objContainer.Build();

            return Container;
        }
    }
}