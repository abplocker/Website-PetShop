using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace PetShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "chi-tiet",
                url: "chi-tiet/{slug}/{id}",
                defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "PetShop.Controllers" }

            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Trangchu", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PetShop.Controllers" }
                );

        }
    }
}
