using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAQMS.Service;
using DAQMS.DomainViewModel;

namespace DAQMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region test SP

            // user service
            UserService userService = new UserService();

            // get all users model
            var lst = userService.GetIById(5);
        
            // save model
            UserViewModel userViewModel = new UserViewModel();

            userViewModel.Id = 0;
            userViewModel.LoginID= "obaidul123";
            userViewModel.UserName = "Obaidul Haque";
            userViewModel.UserPass = "Password";

            userViewModel.UserEmail = "obauidul@asda,com";
            userViewModel.ContactID = "001";
            userViewModel.LoginUserID ="Admin";
            userViewModel.IsActive=true;
            userViewModel.IsLocked=false;
            userViewModel.IsAdmin=true;
            userViewModel.IsChangePassword=false;
            
            int save=  userService.InsertData(userViewModel);

            userViewModel.Id = 2;
            int update = userService.UpdateData(userViewModel);

            userViewModel.Id = 3;
            int delete = userService.DeleteData(userViewModel.Id);

            #endregion test SP
            
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}