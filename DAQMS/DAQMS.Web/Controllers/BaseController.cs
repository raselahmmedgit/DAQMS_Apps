using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DAQMS.Core.Utility;
using DAQMS.Core;
using DAQMS.Domain.Models;

namespace DAQMS.Web
{
    //[UserAuthorize]
    public class BaseController : Controller
    {
        #region Global Variable Declaration

        #endregion

        #region Constructor

        public BaseController()
        {
        }

        #endregion

        #region Action

        #region Action Execute Method

        public User CurrentLoggedInUser
        {
            get
            {
                User user = SessionHelper.CurrentSession.Content.LoggedInUser;
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object objCurrentControllerName = string.Empty;
            this.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);
            object objCurrentActionName = string.Empty;
            this.RouteData.Values.TryGetValue("action", out objCurrentActionName);
            string currentAreaName = string.Empty;
            string currentControllerName = string.Empty;
            string currentActionName = string.Empty;

            if (this.RouteData.DataTokens.ContainsKey("area"))
            {
                currentAreaName = this.RouteData.DataTokens["area"].ToString();
            }
            if (objCurrentControllerName != null)
            {
                currentControllerName = objCurrentControllerName.ToString();
            }
            if (objCurrentActionName != null)
            {
                currentActionName = objCurrentActionName.ToString();
            }

            if (!CheckIfUserIsAuthenticated(filterContext))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            // put whatever data you want which will be sent
                            // to the client
                            message = "Sorry, you are not logged user."
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    if (filterContext.HttpContext.Request.Url != null)
                    {
                        filterContext.Result = new RedirectResult("/Login");
                    }
                }
            }
            else
            {

                if (!CheckIfUserAccessRight(currentActionName, currentControllerName, currentAreaName))
                {
                    if (filterContext.HttpContext.Request.Url != null)
                    {
                        filterContext.Result = new RedirectResult("/Login");
                    }
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }

            base.OnActionExecuting(filterContext);
        }

        #region Check User Authenticated
        private bool CheckIfUserIsAuthenticated(ActionExecutingContext filterContext)
        {
            // If Result is null, we’re OK: the user is authenticated and authorized. 
            if (filterContext.Result == null)
            {
                return true;
            }

            // If here, you’re getting an HTTP 401 status code. In particular,
            // filterContext.Result is of HttpUnauthorizedResult type. Check Ajax here. 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfUserAccessRight(string actionName, string controllerName, string areaName)
        {
            return true;
        }
        #endregion

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureCookie = Request.Cookies["app.culture"];
                if (cultureCookie != null)
                {
                    cultureName = cultureCookie.Value;
                }
                else
                {
                    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                        ? Request.UserLanguages[0]
                        : null; // obtain it from HTTP header AcceptLanguages   
                }
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        #endregion

        #region Common Method

        public string GetModelStateError()
        {
            string errorMessage = string.Empty;

            foreach (var modelStateValue in ViewData.ModelState.Values)
            {
                foreach (var error in modelStateValue.Errors)
                {
                    errorMessage = error.ErrorMessage;
                    break;
                }
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    break;
                }
            }
            return errorMessage;
        }

        public ErrorViewModel GetErrorViewModel(string errorType, string errorMessage)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.ErrorType = errorType;
            errorViewModel.ErrorMessage = errorMessage;
            return errorViewModel;
        }

        #endregion

        #endregion
    }
}