using System;
using System.Web.Http;
using System.Reflection;
using DAQMS.Service;

namespace DAQMS.Web
{
    public static class BootStrapper
    {
        public static void Run()
        {
            //log4net.Config.XmlConfigurator.Configure(new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Web.config")));
        }
        
    }
}