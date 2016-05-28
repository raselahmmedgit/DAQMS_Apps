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
using Code;

namespace DAQMS.Web.Controllers
{

    public class AccountController : BaseController
    {
        #region Global Variable Declaration
        public bool IsChangePasswordFromLogin;
        #endregion

        #region Constructor
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = service ?? new AccountMembershipService();
            IsChangePasswordFromLogin = false;
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }
        public IMembershipService MembershipService
        {
            get;
            private set;
        }
        #endregion

        #region Action


        public ActionResult LogIn()
        {
            return View();
        }

        //
        // POST: /Account/LogIn

        [HttpPost]
        public ActionResult LogIn(LogInViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                UserService userService = new UserService();
                User user = userService.LoadByLoginId(model.UserName);

                if (user != null && user.error_code==0)
                {
                    if (IsValidateUser(model.UserName, model.Password, user))
                    {
                        if (IsActiveUser(user))
                        {
                            if (IsLockedUser(user))
                            {
                               // SetUserClaimsIdentity(user);

                                FormsAuth.SignIn(model.UserName, false);

                               

                                #region save-login-history

                                LoginHistoryService loginHistoryService = new LoginHistoryService();
                                LoginHistoryViewModel loginHistoryViewModel = new LoginHistoryViewModel();
                                loginHistoryViewModel.UserId = user.Id;
                                loginHistoryViewModel.LoginTimestamp = DateTime.Now;
                                loginHistoryViewModel.LogoutTimestamp = DateTime.Now;
                                var SessionId= loginHistoryService.InsertData(loginHistoryViewModel);

                                #endregion save-login-history
                                
                                // Save session
                                BuildUserSession(user.Id, SessionId);

                                SetUserClaimsIdentity(user);

                               if (user.IsChangePassword)
                                {
                                    IsChangePasswordFromLogin = true;
                                    return RedirectToAction("ChangePassword");                               
                                }
                                else
                                {
                                    if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                                     && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                                    {
                                        return Redirect(ReturnUrl);
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

        private void BuildUserSession(int UserId, int SessionId)
        {
            try
            {
                UserService userService = new UserService();
                var User = userService.GetIById(UserId);


                /*   Session["User_Identity_Name"] = string.Format("{0}|{1}|{2}|{3}",
                       User.UserId,         //0
                       User.LoginId,         //1
                       User.LastName,       //2
                       User.Password         // 3
                       );
               */
                Session["User_Identity_Name"] = User.UserName;
                Session["LoginId"] = User.LoginID;
                Session["SessionId"] = SessionId;

                //FormsAuthentication.RedirectFromLoginPage(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                //    userList[0].NUMUSERID,         //0
                //    userList[0].STRUSERID,         //1
                //    userList[0].STRUSERNAME,       //2
                //    STRPASSWORD,                   //3
                //    userList[0].ISADMIN,           //4
                //    userList[0].ISVIEWALLREPORT,   //5
                //    userList[0].numRoleID,      //6
                //    userList[0].STRDESIGNATION,    //7
                //    userList[0].STRDEPARTMENTNAME, //8
                //    userList[0].EMAILADDRESS,       //9
                //     userList[0].STRROLENAME //10
                //    ), true);

                #region UserJob
               // DAL.Tasks.UserJobDAL obj = new DAL.Tasks.UserJobDAL();
             //   string userJob = obj.SaveObj(User.LoginId);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            int Id = (int)(Session["SessionId"]);

                LoginHistoryService loginHistoryService = new LoginHistoryService();
                LoginHistoryViewModel loginHistoryViewModel = new LoginHistoryViewModel();
                loginHistoryViewModel.Id = Id;
                loginHistoryService.UpdateData(loginHistoryViewModel);

            Session.Abandon();
            Session.Clear();
            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuth.SignOut();

            return RedirectToAction("LogIn", "Account");
        }

        private bool IsValidateUser(string userName, string password, User user)
        {
            if (user.LoginID.ToLower() == userName.ToLower() && Password.verifyMd5Hash(password,user.UserPass))
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

            RoleService roleService = new RoleService();

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
            int Id = (int)(Session["SessionId"]);

            //Session["SessionId"] = SessionId;
            User loggedInUser = SessionHelper.CurrentSession.Content.LoggedInUser;
            if (loggedInUser == null)
            {
                LoginHistoryService loginHistoryService = new LoginHistoryService();
                LoginHistoryViewModel loginHistoryViewModel = new LoginHistoryViewModel();
                loginHistoryViewModel.Id = Id;
                loginHistoryViewModel.UserId = loggedInUser.Id;
                loginHistoryViewModel.LoginTimestamp = DateTime.Now;
                loginHistoryViewModel.LogoutTimestamp = DateTime.Now;
                loginHistoryService.UpdateData(loginHistoryViewModel);
            }

            Session.RemoveAll();
            SessionHelper.CurrentSession.Remove("LoggedInUser");
            SessionHelper.CurrentSession.Clear();

            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuth.SignOut();

            return RedirectToAction("LogIn", "Account");
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
            UserService userService = new UserService();
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            var user = userService.LoadByLoginId(User.Identity.Name);
            ViewBag.User = user;
            ChangePasswordModel model = new ChangePasswordModel();
            model.OldPassword = "";
            return View(model);        
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
                    if (IsChangePasswordFromLogin)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("ChangePasswordSuccess");
                    }
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
                UserService userService = new UserService();
                UserViewModel userViewModel = userService.GetIById(user.Id);
      
                userViewModel.IsChangePassword = false;
                int update = userService.UpdateData(userViewModel);
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

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private CustomMembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(CustomMembershipProvider provider)
        {
            _provider = provider ?? new CustomMembershipProvider();
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return CustomMemberShip.Provider.ChangePassword(userName, oldPassword, newPassword);
            //MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            //return currentUser.ChangePassword(oldPassword, newPassword);
        }
    }
    
}