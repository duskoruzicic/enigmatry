using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Web.Mvc;

namespace Vendor.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var webApiResolver = new AutofacWebApiDependencyResolver(ContainerInitializer.GetContainer());
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
    }
}
