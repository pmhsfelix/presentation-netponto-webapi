using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MemoryHosting
{
    public class HelloController : ApiController
    {
        public string Get()
        {
            return "Hello Web";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "default",
                "{controller}/{id}",
                new { controller = "home", id = RouteParameter.Optional });




            var server = new HttpServer(config);
            var client = new HttpClient(server);



            var resp = client.GetAsync("http://does.not.matter/hello")
                .Result;
                
            Console.WriteLine(resp.Content.ReadAsStringAsync().Result);
        }
    }
}
