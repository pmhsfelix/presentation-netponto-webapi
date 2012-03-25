using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace First.Web.Controllers
{
    public class TimeController : ApiController
    {
        public string Get()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}
