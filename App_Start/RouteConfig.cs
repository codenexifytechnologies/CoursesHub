using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoursesHub
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "BlogList",
                url: "blog",
                defaults: new { controller = "Blog", action = "Blog" }
            );

            routes.MapRoute(
                name: "BlogDetailSEO",
                url: "blog/{slug}",
                defaults: new { controller = "Blog", action = "BlogDetail" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "CoursesHub.Controllers" }
            );
        }
    }
}
