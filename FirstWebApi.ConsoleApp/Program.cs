using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace FirstWebApi.ConsoleApp
{
    public class HomeController : ApiController
    {
        
        public HttpResponseMessage Get()
        {
            var ua = Request.Headers.UserAgent.First().Product;
            return new HttpResponseMessage()
                       {
                           Content = new StringContent("hello web, "+ua)
                       };
        }

        public HttpResponseMessage Get(int id)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("hello web, with id = "+id)
            };
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080/");
            config.Routes.MapHttpRoute(
                "default",
                "{controller}/{id}",
                new {id = RouteParameter.Optional }
                );

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            Console.WriteLine("Server is opened");
            Console.ReadKey();


        }
    }
}
