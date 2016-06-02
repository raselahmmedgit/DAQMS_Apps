using Code;
using DAQMS.Core;
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
    public class AlertViewController : Controller
    {
        private readonly AlertHistoryService _AlertHistoryService;
        public AlertViewController()
        {
            this._AlertHistoryService = new AlertHistoryService();
        }

        public ActionResult Index()
        {
            AlertHistoryViewModel model = new AlertHistoryViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("Project");
            model.AlertTypeList = PopulateDropdown.PopulateDropdownListByTable("alert_type");
            //model.AlertStatusList = PopulateDropdown.PopulateDropdownReadStatusList();
            return View("Index", model);
        }

        public PartialViewResult List(int? page)
        {
            IList<AlertHistoryViewModel> model = _AlertHistoryService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, AlertHistoryViewModel model)
        {
            IList<AlertHistoryViewModel> data = _AlertHistoryService.GetByItem(model);
            return PartialView("_PartialList", data);
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            AlertHistoryViewModel model = _AlertHistoryService.GetIById(id);
            return View("View", model);
        }
    
        [HttpPost]
        public ActionResult View(AlertHistoryViewModel model)
        {
            int returnId = -1;
            string strMessage = string.Empty;
            model.LoginUserID = HttpContext.User.Identity.Name;

            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(strMessage))
                    {
                        returnId = _AlertHistoryService.UpdateData(model);

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
    }
}