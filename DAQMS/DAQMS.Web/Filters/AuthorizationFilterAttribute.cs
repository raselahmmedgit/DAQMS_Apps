using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAQMS.DomainViewModel;


namespace Filters
{
    public class AuthorizationFilterAttribute : AuthorizeAttribute
    {
        #region Fields

        private string controller;
        private string action;
        private string requestType;

        #endregion

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var request = httpContext.Request;
            requestType = request.HttpMethod.ToString();
            controller = Convert.ToString(request.RequestContext.RouteData.Values["controller"] ?? request["controller"]);
            action = Convert.ToString(request.RequestContext.RouteData.Values["action"] ?? request["action"]);
            int userId = 0;

            try
            {
                userId = Convert.ToInt32(request.RequestContext.RouteData.Values["Id"] ?? request["Id"]);
            }
            catch { }

            if (controller.Equals("Account") || (controller.Equals("Home") && action.Equals("Public")))
            {
                return true;
            }


            var isAuthorized = base.AuthorizeCore(httpContext);

            if (isAuthorized)
            {
                var currentUser = httpContext.User.Identity.Name;

                return ValidatePageUrl(controller, action, currentUser, "");
            }
            return isAuthorized;
        }

        private bool ValidatePageUrl(string controller, string action, string currentUser, string empId)
        {
            if (controller.Contains("Home") && action.Contains("Index")) return true;

            bool flag = true;

            //User user = (User)Code.AppConstant.User;
            //if (user != null)
            //{
            //    if (currentUser.Equals(user.LoginId) && controller.Equals("Employee") && action.Equals("EmploymentInfoIndex") && (empId == user.EmpId))
            //    {
            //        return true;
            //    }
            //}

            var currentMenus = (List<UserRightViewModel>)HttpContext.Current.Session["CurrentMenus"];

            //session out then user compel to signout
            if (currentMenus == null)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                return false;
            }

            var visitingMenu = currentMenus.Find(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.PageUrl.ToLower().Contains(action.ToLower()));
            //var visitingMenu = currentMenus.Find(x => x.PageUrl.ToLower().Equals(controller.ToLower()) && x.PageUrl.ToLower().Contains(action.ToLower()));

            if (visitingMenu == null && !action.Contains("Index"))
                visitingMenu = (UserRightViewModel)HttpContext.Current.Session["CurrentMenu"];

            if (currentMenus != null && action.Contains("Index")) //check view permission
            {
                flag = visitingMenu != null ? visitingMenu.OptView : false;//currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsAssignedMenu);
            }
            //else if (currentMenus != null && action.Contains("Create") && requestType.Contains("GET")) // check view permission
            //{
            //    flag = visitingMenu != null ? (visitingMenu.IsAssignedMenu | visitingMenu.IsAddAssigned) : false;//currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsAddAssigned);
            //}
            else if (currentMenus != null && action.Contains("Create") && requestType.Contains("POST")) // check add permission
            {
                flag = visitingMenu != null ? visitingMenu.OptAdd : false;//currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsAddAssigned);
            }
            else if (currentMenus != null && action.Contains("Edit") && requestType.Contains("POST")) // check update permission
            {
                flag = visitingMenu != null ? visitingMenu.OptUpdate : currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.OptUpdate);
            }
            else if (currentMenus != null && action.Contains("Delete")) // check delete permission
            {
                flag = visitingMenu != null ? visitingMenu.OptDelete : currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.OptDelete);
            }

            if (currentMenus != null && currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.PageUrl.ToLower().Contains(action.ToLower())))
                HttpContext.Current.Session["CurrentMenu"] = visitingMenu; //currentMenus.Find(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.PageUrl.ToLower().Contains(action.ToLower()));

            return flag;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                filterContext.Result = new RedirectResult("~/Account/LogOn");



            /*   else
               {
                   UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                   filterContext.Result = new RedirectResult(urlHelper.Action("Unauthorized", "Home"));
               }
             * */
        }
    }
}