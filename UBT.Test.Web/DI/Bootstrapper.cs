using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Http;
using UBTTest.Business;
using UBTTest.Web.Controllers.MVC;
using UBTTest.Web.Controllers.API;

namespace UBTTest.Web.DI
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // register dependency resolver for WebAPI RC
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            // if you still use the beta version - change above line to:
            //GlobalConfiguration.Configuration.ServiceResolver.SetResolver(new Unity.WebApi.UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            

            ServiceLayerBootstrapper.RegisterTypes(container);

            container.RegisterType<HomeController>();
            container.RegisterType<DataApiController>();
            return container;
        }
    }
}