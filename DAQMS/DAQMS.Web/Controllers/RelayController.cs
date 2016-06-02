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
    public class RelayController : Controller
    {
        private readonly RelayService _relayService;

        public RelayController()
        {
            this._relayService = new RelayService();
        }

        public ActionResult Index()
        {
            RelayViewModel model = new RelayViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("project");
            model.RelayStateList = PopulateDropdown.PopulateDropdownListByTable("relay_state");
            model.DeviceIDList = PopulateDropdown.PopulateDropdownListByTable("devices");
            model.DeviceStateList = PopulateDropdown.PopulateDropdownListByTable("device_status");


            return View(model);
        }

        public PartialViewResult List(int? page)
        {
            IList<RelayViewModel> model = _relayService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, RelayViewModel model)
        {
            IList<RelayViewModel> data = _relayService.GetByItem(model);
            return PartialView("_PartialList", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            RelayViewModel model = new RelayViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("project");
            model.RelayStateList = PopulateDropdown.PopulateDropdownListByTable("relay_state");
            model.DeviceIDList = PopulateDropdown.PopulateDropdownListByTable("devices");
            model.DeviceStateList = PopulateDropdown.PopulateDropdownListByTable("device_status");
            model.RelayStatusList = PopulateDropdown.PopulateDropdownListByTable("relay_status");

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RelayViewModel model)
        {
            int returnId = -1;
            model.LoginUserID = HttpContext.User.Identity.Name;
            var strMessage = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(strMessage))
                    {
                        returnId = _relayService.InsertData(model);

                        if (returnId <= 0)
                        {
                            strMessage = "Error: There was a problem while processing your request.";
                            return Content(strMessage);
                        }
                        else
                        {
                            return Content(Boolean.TrueString);
                        }
                    }
                    else
                    {
                        return Content(strMessage);
                    }
                }
                else
                {
                    strMessage = Common.GetModelStateErrorMessage(ModelState);
                    return Content(strMessage);
                }
            }
            catch (Exception ex)
            {
                strMessage = "Error: There was a problem while processing your request: " + ex.Message;
                return Content(strMessage);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            RelayViewModel model = _relayService.GetIById(id);
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("project", model.CompanyId);
            model.RelayStateList = PopulateDropdown.PopulateDropdownListByTable("relay_state");
            model.DeviceIDList = PopulateDropdown.PopulateDropdownListByTable("devices", model.ProjectId);
            model.DeviceStateList = PopulateDropdown.PopulateDropdownListByTable("device_status");
            model.RelayStatusList = PopulateDropdown.PopulateDropdownListByTable("relay_status");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RelayViewModel model)
        {
            int returnId = -1;
            model.LoginUserID = HttpContext.User.Identity.Name;
            var strMessage = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(strMessage))
                    {
                        returnId = _relayService.UpdateData(model);

                        if (returnId <= 0)
                        {
                            strMessage = "Error: There was a problem while processing your request.";
                            return Content(strMessage);
                        }
                        else
                        {
                            return Content(Boolean.TrueString);
                        }
                    }
                    else
                    {
                        return Content(strMessage);
                    }
                }
                else
                {
                    strMessage = Common.GetModelStateErrorMessage(ModelState);
                    return Content(strMessage);
                }
            }
            catch (Exception ex)
            {
                strMessage = "Error: There was a problem while processing your request: " + ex.Message;
                return Content(strMessage);
            }
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            int userId = -1;
            bool result = true;
            string errMsg = string.Empty;
            try
            {

                userId = _relayService.DeleteData(id);

                if (userId <= 0)
                {
                    result = false;
                    errMsg = "This information is already used in another scope.";
                }
                else
                {
                    result = true;
                    errMsg = "Information has been deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }
            return Json(new
            {
                Success = result,
                Message = errMsg
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyWiseProject(int companyId)
        {
            ProjectService _ProjectService = new ProjectService();
            IList<SelectListItem> list = new List<SelectListItem>();

            ProjectViewModel projectModel = new ProjectViewModel();
            projectModel.CompanyId = companyId;

            List<ProjectViewModel> projectList = _ProjectService.GetByItem(projectModel);

            if (projectList.Count > 0)
            {
                foreach (var item in projectList)
                {
                    list.Add(new SelectListItem { Text = item.ProjectName, Value = item.Id.ToString() });
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProjectWiseDevice(int projectId)
        {
            DeviceService _deviceService = new DeviceService();

            IList<SelectListItem> list = new List<SelectListItem>();

            DeviceViewModel model = new DeviceViewModel();
            model.ProjectId = projectId;

            List<DeviceViewModel> itemList = _deviceService.GetByItem(model);

            if (itemList.Count > 0)
            {
                foreach (var item in itemList)
                {
                    list.Add(new SelectListItem { Text = item.DeviceID, Value = item.Id.ToString() });
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyWiseContact(int companyId)
        {
            ContactService _contactService = new ContactService();

            IList<SelectListItem> list = new List<SelectListItem>();

            ContactViewModel model = new ContactViewModel();
            model.CompanyId = companyId;

            List<ContactViewModel> itemList = _contactService.GetByItem(model);

            if (itemList.Count > 0)
            {
                foreach (var item in itemList)
                {
                    list.Add(new SelectListItem { Text = item.ContactName, Value = item.Id.ToString() });
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}