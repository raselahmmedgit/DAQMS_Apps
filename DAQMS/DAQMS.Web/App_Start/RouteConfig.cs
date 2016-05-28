using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAQMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "LogIn", id = UrlParameter.Optional }
            );

           // routes.MapRoute(
           //     name: "LoginRoute",
           //     url: "Login",
           //     defaults: new { controller = "Account", action = "LogIn" }
           // );
           // routes.MapRoute(
           //     name: "LogoffRoute",
           //     url: "Logout",
           //     defaults: new { controller = "Account", action = "LogOut" }
           // );
           // routes.MapRoute(
           //    name: "RegisterRoute",
           //    url: "Register",
           //    defaults: new { controller = "Account", action = "Register" }
           //);
           // routes.MapRoute(
           //    name: "ForgetPasswordRoute",
           //    url: "Forgetpassword",
           //    defaults: new { controller = "User", action = "ForgetPassword" }
           //);

           // routes.MapRoute(
           //     name: "Default",
           //     url: "{controller}/{action}/{id}",
           //     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           // );
        }
    }
}
