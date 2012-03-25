using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Common
{
    public class ParentChildControllerFactory : IHttpControllerFactory
    {
        private readonly IHttpControllerFactory _innerFactory;

        public ParentChildControllerFactory(IHttpControllerFactory innerFactory)
        {
            _innerFactory = innerFactory;
        }

        public IHttpController CreateController(HttpControllerContext controllerContext, string controllerName)
        {
            var values = controllerContext.RouteData.Values;
            object parent;
            if (values.TryGetValue("parent", out parent))
            {
                controllerName = parent.ToString() + controllerName;
            }
            var controller = _innerFactory.CreateController(controllerContext, controllerName);
            return controller;
        }

        public void ReleaseController(IHttpController controller)
        {
            _innerFactory.ReleaseController(controller);
        }
    }
}
