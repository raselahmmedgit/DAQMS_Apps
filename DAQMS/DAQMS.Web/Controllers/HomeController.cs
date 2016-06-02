using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAQMS.Core;
using DAQMS.Service;
using DAQMS.DomainViewModel;
using Helper;

namespace DAQMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //#region test SP

            ////  service
            //TempSensorServcie Service = new TempSensorServcie();

            //// model
            //TempSensorViewModel model = new TempSensorViewModel();
            //model.CompanyId = 1;
            //model.ProjectId = 1;
            //model.DeviceId = 2;
            //model.DateRangeFrom = "2016-05-01";
            //model.DateRangeTo = "2016-05-25";
            //model.ValueType = "max";

            //// get data
            //var lst = Service.GetByItem(model);

            //#endregion test SP
            
            return View();
        }

        public ActionResult Error()
        {
            ErrorViewModel errorViewModel = (ErrorViewModel)SessionHelper.CurrentSession.Get("Error");
            SessionHelper.CurrentSession.Remove("Error");
            return View(errorViewModel);
        }

        [NoCache]
        public ActionResult LeftMenu()
        {
            UserRightService _service=new UserRightService();

            List<UserRightViewModel> model = new List<UserRightViewModel>();
            UserViewModel usr=new UserViewModel();

            usr.Id=1;
            usr.LoginUserID="Admin";

            model=_service.GetItemByUser(usr);

            /*
            model.MenuList = PrepareMenu(AppConstraint.ModuleName); // context.GetMenus(User.Identity.Name.ToString(), AppConstraint.ApplicationName, AppConstraint.ModuleName);

            if (string.Compare(User.Identity.Name.Trim(), AppConstraint.SystemInitializer, true) != 0)
            {
                int intCount = model.MenuList.Count;
                int index = 0;

                while (index < intCount)
                {
                    if (string.Compare(model.MenuList[index].MenuName, "Menu", true) == 0 || string.Compare(model.MenuList[index].MenuName, "Application", true) == 0 || string.Compare(model.MenuList[index].MenuName, "Module", true) == 0)
                    {
                        model.MenuList.RemoveAt(index);
                        intCount--;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            */

            model = model.OrderBy(m => m.SerialNo).ToList();

            return PartialView("_MenuSidebarPartial", model);
        }
      
        [NoCache]
        public ActionResult GettingSiteMap()
        {
            UserRightService _service = new UserRightService();

            UserRightViewModel menu = new UserRightViewModel();
            List<UserRightViewModel> model = new List<UserRightViewModel>();

            UserViewModel usr = new UserViewModel();

           // MenuFormViewModel model = new MenuFormViewModel();

            string actionName = System.Web.HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("action");
            string controllerName = System.Web.HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");

            usr.Id = 1;
            usr.LoginUserID = "Admin";

            model = _service.GetItemByUser(usr);

        //    model.MenuList = PrepareMenu(AppConstraint.ModuleName).ToList();
            var selectedMenu = (List<UserRightViewModel>)model;
            menu = selectedMenu.Find(x => x.PageUrl.ToLower().Contains(controllerName.ToLower()) && x.PageUrl.ToLower().Contains(actionName.ToLower()));

            if (menu == null)
            {
                menu = new UserRightViewModel();
                menu = (from t in selectedMenu
                              where t.PageUrl.ToLower().Contains(controllerName.ToLower())
                              select t).LastOrDefault();
            }
            return PartialView("_BreadCrumbs", menu);
        }
    }
}