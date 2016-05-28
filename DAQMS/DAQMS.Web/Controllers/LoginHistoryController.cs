using Code;
using DAQMS.DomainViewModel;
using DAQMS.Service;
using DAQMS.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAQMS.Web.Controllers
{
    public class LoginHistoryController : Controller
    {
         private readonly LoginHistoryService _loginHistoryervice;

         public LoginHistoryController()
        {
            this._loginHistoryervice = new LoginHistoryService();
        }

        public ActionResult Index()
        {
            LoginHistoryViewModel model = new LoginHistoryViewModel();            
            return View(model);
        }

        public PartialViewResult List(int? page)
        {
            IList<LoginHistoryViewModel> model = _loginHistoryervice.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, LoginHistoryViewModel model)
        {
            IList<LoginHistoryViewModel> data = _loginHistoryervice.GetByItem(model);
            return PartialView("_PartialList", data);
        }
	}
}