using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace First.Web.Controllers
{
    public class ParentChildController : ApiController
    {
        public string Get(int id)
        {
            return string.Format("{0}", id);
        }
        
        public string Get(int id, int childId)
        {
            return string.Format("{0}:{1}", id, childId);
        }
    }
}