using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace First
{

    public class HomeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var userAgent = Request.Headers.UserAgent.Count != 0 ? 
                Request.Headers.UserAgent.First().Product.Name 
                : "stranger";
            return new HttpResponseMessage()
                       {
                           Content = new StringContent("Hello there, "+userAgent)
                       };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");
            config.Routes.MapHttpRoute(
                "default",
                "{controller}/{id}",
                new {controller = "home", id = RouteParameter.Optional});

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.WriteLine("Server is opened");
            Console.ReadKey();
        }
    }
}
