using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using BookShopModel;
using BookShopModel.Model;
using Microsoft.Practices.Unity;
using BookShopModel.Interfaces;
using BookShopModel.ContainerEF;

namespace BookShopWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class UnityControllerFactory : DefaultControllerFactory
    {
        IUnityContainer _container;
        public UnityControllerFactory(IUnityContainer container)
        {
            _container = container;
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)_container.Resolve(controllerType);
        }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
        protected void Application_Start()
        {
            var container = new UnityContainer();
            container.RegisterType<IModelContainer, ModelContainerEF>();
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));

            Database.SetInitializer(new EFInitializer());
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_End()
        {
        }
    }
}