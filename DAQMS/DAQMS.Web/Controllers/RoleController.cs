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
    public class RoleController : Controller
    {
        private readonly RoleService _roleService;

        public RoleController()
        {
            this._roleService = new RoleService();
        }

        public ActionResult Index()
        {
            RoleViewModel model = new RoleViewModel();
            model.ModuleList = PopulateDropdown.PopulateDropdownListByTable("modules");
            return View(model);
        }

        public PartialViewResult List(int? page)
        {
            IList<RoleViewModel> model = _roleService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, RoleViewModel model)
        {
            IList<RoleViewModel> data = _roleService.GetByItem(model);
            return PartialView("_PartialList", data);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            RoleViewModel model = new RoleViewModel();
            model.ModuleList = PopulateDropdown.PopulateDropdownListByTable("modules");
            return View(model);
        }
	}
}