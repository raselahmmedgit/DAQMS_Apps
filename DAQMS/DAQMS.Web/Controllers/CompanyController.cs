using Code;
using DAQMS.DomainViewModel;
using DAQMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAQMS.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _CompanyService;

        public CompanyController()
        {
            this._CompanyService = new CompanyService();
        }

        public ActionResult Index()
        {
            CompanyViewModel model = new CompanyViewModel();
            return View(model);
        }

        public PartialViewResult List(int? page)
        {
            IList<CompanyViewModel> model = _CompanyService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, CompanyViewModel model)
        {
            IList<CompanyViewModel> data = _CompanyService.GetByItem(model);
            return PartialView("_PartialList", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CompanyViewModel model = new CompanyViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
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
                        returnId = _CompanyService.InsertData(model);

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
            CompanyViewModel model = _CompanyService.GetIById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CompanyViewModel model)
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
                        returnId = _CompanyService.UpdateData(model);

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

                userId = _CompanyService.DeleteData(id);

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