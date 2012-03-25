using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using Common;

namespace First.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var config = GlobalConfiguration.Configuration;

            //config.Formatters.Clear();
            config.Formatters.Add(new ImageFromTextFormatter());
            config.Formatters.Add(new WaveFromTextFormatter());
            config.Formatters.Add(new CsvMediaTypeFormatter());

            var controllerFactory = config.ServiceResolver.GetHttpControllerFactory();
            config.ServiceResolver.SetService(typeof(IHttpControllerFactory), new ParentChildControllerFactory(controllerFactory));

            config.Routes.MapHttpRoute(
                "parent", 
                "api/{parent}/{id}/{controller}/{childId}", 
                new { childId = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                "ApiDefault",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});

            var counter = new Counter();
            config.MessageHandlers.Add(new RequestCountHandler(counter));
            //config.MessageHandlers.Add(new ApiKeyMessageHandler());

            //config.ServiceResolver.SetService(typeof (CounterController), new CounterController(counter));
            config.ServiceResolver.SetResolver( 
                type => type == typeof(CounterController) ? new CounterController(counter) : null,
                type => new object[0]
                );
            

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}