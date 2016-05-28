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
    public class DeviceController : Controller
    {
        private readonly DeviceService _DeviceService;

        public DeviceController()
        {
            this._DeviceService = new DeviceService();
        }

        public ActionResult Index()
        {
            DeviceViewModel model = new DeviceViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("Company");

            return View(model);
        }

        public PartialViewResult List(int? page)
        {
            IList<DeviceViewModel> model = _DeviceService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, DeviceViewModel model)
        {
            IList<DeviceViewModel> data = _DeviceService.GetByItem(model);
            return PartialView("_PartialList", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            DeviceViewModel model = new DeviceViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("Company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("Project");
            model.DeviceStatusList = PopulateDropdown.PopulateDropdownListByTable("DeviceStatus");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DeviceViewModel model)
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
                        returnId = _DeviceService.InsertData(model);

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
            DeviceViewModel model = _DeviceService.GetIById(id);
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("Company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("Project");
            model.DeviceStatusList = PopulateDropdown.PopulateDropdownListByTable("DeviceStatus");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DeviceViewModel model)
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
                        returnId = _DeviceService.UpdateData(model);

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

                userId = _DeviceService.DeleteData(id);

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
    }
}