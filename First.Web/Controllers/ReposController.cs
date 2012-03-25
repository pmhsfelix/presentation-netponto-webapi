using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace First.Web.Controllers
{
    public class ReposController : ApiController
    {

        public JsonArray Get()
        {
            using (var client = new HttpClient())
            {
                var res = client.GetAsync("https://api.github.com/users/pmhsfelix/repos").Result;
                return res.Content.ReadAsAsync<JsonArray>().Result;
            }
        }

    }
}