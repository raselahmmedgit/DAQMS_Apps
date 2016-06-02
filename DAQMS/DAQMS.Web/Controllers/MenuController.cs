using DAQMS.Domain.Models;
using DAQMS.DomainViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAQMS.Web;
using DAQMS.Service;

namespace DAQMS.Web.Areas.UM.Controllers
{
    public class MenuController : Controller
    {
        private readonly UserRightService _UserRightService;

        public MenuController()
        {
            this._UserRightService = new UserRightService();
        }

        #region Properties
        private List<Menu> _menus
        {
            get
            {
                if (Session["CurrentMenus"] != null)
                    return (List<Menu>)Session["CurrentMenus"];
                else
                    return new List<Menu>();
            }
            set
            {
                Session["CurrentMenus"] = value;
            }
        }
        #endregion

        // GET: UM/Menu
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult MenuSideBar()
        {
            UserViewModel user = new UserViewModel();
            user.LoginUserID = "administrator";  /// Session Value is needed
            user.Id = 1; /// Session Value is needed
            var menuObj = _UserRightService.GetItemByUser(user).ToList();
            return PartialView("_MenuSidebarPartial", menuObj);
        }

        
    }
}