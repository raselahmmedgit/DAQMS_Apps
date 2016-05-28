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
    public class UserRegistrationController : Controller
    {
        private readonly UserService _UserService;
        private readonly UserRoleService _UserRoleService;
        
        public UserRegistrationController()
        {
            this._UserService = new UserService();
            this._UserRoleService = new UserRoleService();
        }

        public ActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            return View(model);
        }

        [HttpGet]
        public PartialViewResult List(int? page)
        {
            IList<UserViewModel> model = _UserService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, UserViewModel model)
        {
            IList<UserViewModel> data = _UserService.GetByItem(model);
            return PartialView("_PartialList", data);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            UserViewModel model = new UserViewModel();
            model.UserRoleList = new List<UserRoleViewModel>();
            model.UserRoleList = _UserRoleService.GetAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            int userId = -1, roleId = -1;
            model.LoginUserID = HttpContext.User.Identity.Name;
            var strMessage = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(strMessage))
                    {

                        userId = _UserService.InsertData(model);

                        if (userId <= 0)
                        {
                            strMessage = "Error: There was a problem while processing your request.";
                            return Content(strMessage);
                        }
                        else
                        {
                            if (model.UserRoleList.Where(x => x.IsAssignRole == true).ToList().Count > 0)
                            {
                                foreach (var item in model.UserRoleList.Where(x => x.IsAssignRole == true))
                                {
                                    try
                                    {
                                        item.UserId = userId;
                                        item.LoginUserID = HttpContext.User.Identity.Name;
                                        roleId = _UserRoleService.InsertData(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        strMessage = "Error: There was a problem while processing your request: " + ex.Message;
                                        return Content(strMessage);
                                    }
                                }
                            }
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
            UserViewModel model = _UserService.GetIById(id);
            ModelInitializer(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            int userId = -1, roleId = -1;
            model.LoginID = HttpContext.User.Identity.Name;
            var strMessage = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {

                    if (string.IsNullOrEmpty(strMessage))
                    {
                        userId = _UserService.UpdateData(model);

                        if (userId <= 0)
                        {
                            strMessage = "Error: There was a problem while processing your request.";
                            return Content(strMessage);
                        }
                        else
                        {
                            if (model.UserRoleList.Count > 0)
                            {
                                if (userId > 0)
                                {
                                    foreach (var item in model.UserRoleList)
                                    {
                                        try
                                        {
                                            item.UserId = userId;
                                            item.LoginUserID = HttpContext.User.Identity.Name;
                                            roleId = _UserRoleService.UpdateData(item);
                                        }
                                        catch (Exception ex)
                                        {
                                            strMessage = "Error: There was a problem while processing your request: " + ex.Message;
                                            return Content(strMessage);
                                        }
                                    }
                                }
                            }
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

                userId = _UserService.DeleteData(id);

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

        private void ModelInitializer(UserViewModel model)
        {
            model.UserRoleList = model.UserRoleList = new List<UserRoleViewModel>();
            model.UserRoleList = _UserRoleService.GetAll();
            List<UserRoleViewModel> roleList = (from tr in model.UserRoleList
                                                where tr.UserId == model.Id
                                                select tr).ToList();
            if (model.UserRoleList.Count > 0)
            {
                foreach (var item in model.UserRoleList)
                {
                    if (item.UserId > 0)
                    {
                        foreach (var itemRole in roleList)
                        {
                            if (itemRole.UserId == item.UserId) item.IsAssignRole = true;
                        }
                    }
                }
            }
        }

    }
}
