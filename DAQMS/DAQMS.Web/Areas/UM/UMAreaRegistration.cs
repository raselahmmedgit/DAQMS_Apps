using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAQMS.Web.Areas.UM
{
    public class UMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AppAccess";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AppAccess_default",
                "AppAccess/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}