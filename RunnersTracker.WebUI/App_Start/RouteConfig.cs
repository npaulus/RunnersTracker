﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RunnersTracker.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Register",
                url: "register/{action}/{code}",
                defaults: new { controller = "Register", action = "Register", code = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "login/{action}/{code}",
                defaults: new { controller = "Login", action = "Login", code = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "Home/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );                     

            routes.MapRoute(
                name: "RunningLogPage",
                url: "RunningLog/Index/{page}",
                defaults: new { controller = "RunningLog", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RunningLogModify",
                url: "RunningLog/{action}/{logId}",
                defaults: new { controller = "RunningLog" }
            );

            routes.MapRoute(
                name: "Summary",
                url: "summary/{action}",
                defaults: new { controller = "Summary", action = "Summary" }
            );        

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}