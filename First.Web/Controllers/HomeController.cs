using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace First.Web.Controllers
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
                Content = new StringContent("Hello there, " + userAgent)
            };
        }
    }
}