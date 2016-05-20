using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DAQMS.Core;
using DAQMS.Domain.Models;
using DAQMS.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DAQMS.Web.Models;
using DAQMS.DomainViewModel;

namespace DAQMS.Web.Controllers
{

    public class AccountController : BaseController
    {
        #region Global Variable Declaration
        private readonly UserService _userService = new UserService();
        private readonly LoginHistoryService _loginHistoryService = new LoginHistoryService();
        private readonly RoleService _roleService = new RoleService();
        #endregion

        #region Constructor

        #endregion

        #region Action


        public ActionResult LogIn()
        {
            return View();
        }

        //
        // POST: /Account/LogIn

        [HttpPost]
        public ActionResult LogIn(LogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                
                User user = _userService.LoadByLoginId(model.UserName);

                if (user != null)
                {
                    if (IsValidateUser(model.UserName, model.Password, user))
                    {
                        if (IsActiveUser(user))
                        {
                            if (IsLockedUser(user))
                            {
                                SetUserClaimsIdentity(user);
                                
                                LoginHistoryViewModel loginHistoryViewModel = new LoginHistoryViewModel();
                                loginHistoryViewModel.UserId = user.Id;
                                loginHistoryViewModel.LoginTimestamp = DateTime.Now;
                                loginHistoryViewModel.LogoutTimestamp = DateTime.Now;
                                _loginHistoryService.InsertData(loginHistoryViewModel);

                                if (user.IsChangePassword)
                                {
                                    return RedirectToAction("ChangePassword", "Account"); 
                                }
                                else
                                {
                                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                     && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                                    {
                                        return Redirect(returnUrl);
                                    }
                                    else
                                    {
                                        return RedirectToAction("Index", "Home");
                                    } 
                                }
                                
                            }
                            else
                            {
                                ModelState.AddModelError("", "The user is locked.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "The user is inactive.");
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user not register, please register first.");
                }


            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool IsValidateUser(string userName, string password, User user)
        {
            if (user.LoginID == userName && Password.verifyMd5Hash(user.UserPass, password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool IsActiveUser(User user)
        {
            if (user.IsActive == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool IsLockedUser(User user)
        {

            if (user.IsLocked == false)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
         
        private void SetUserClaimsIdentity(User user)
        {
            SessionHelper.CurrentSession.Content.LoggedInUser = user;

            

            var ident = new ClaimsIdentity(
          new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, user.UserName),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

              new Claim(ClaimTypes.Name,user.UserName),
              
              // optionally you could add roles if any
              //new Claim(ClaimTypes.Role, "Admin")

          },
          DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(
               new AuthenticationProperties { IsPersistent = false }, ident);
        }

        //
        // GET: /Account/LogOut
        [UserAuthorize]
        public ActionResult LogOut()
        {
            User loggedInUser = SessionHelper.CurrentSession.Content.LoggedInUser;
            if (loggedInUser == null)
            {
                LoginHistoryViewModel loginHistoryViewModel = new LoginHistoryViewModel();
                loginHistoryViewModel.UserId = loggedInUser.Id;
                loginHistoryViewModel.LoginTimestamp = DateTime.Now;
                loginHistoryViewModel.LogoutTimestamp = DateTime.Now;
                _loginHistoryService.InsertData(loginHistoryViewModel);
            }

            Session.RemoveAll();
            SessionHelper.CurrentSession.Remove("LoggedInUser");
            SessionHelper.CurrentSession.Clear();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //UserViewModel userViewModel = new UserViewModel();
                    //UserService userService = new UserService();
                    //userService.InsertData(userViewModel);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [UserAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [UserAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    SetIsChangePassword();
                    changePasswordSucceeded = true;
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void SetIsChangePassword()
        {
            User user = SessionHelper.CurrentSession.Content.LoggedInUser;
            if (user != null)
            {
                UserViewModel userViewModel = new UserViewModel();

                userViewModel.Id = user.Id;
                userViewModel.IsChangePassword = false;
                int update = _userService.UpdateData(userViewModel);
            }

        }


        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        #endregion

    }
}