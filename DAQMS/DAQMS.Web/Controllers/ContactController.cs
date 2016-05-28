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
    public class ContactController : Controller
    {
       private readonly ContactService _ContactService;

       public ContactController()
        {
            this._ContactService = new ContactService();
        }

        public ActionResult Index()
        {
            ContactViewModel model = new ContactViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ContactTypeList = PopulateDropdown.PopulateDropdownListByTable("contact_type");
            return View(model);
        }

        public PartialViewResult List(int? page)
        {
            IList<ContactViewModel> model = _ContactService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, ContactViewModel model)
        {
            IList<ContactViewModel> data = _ContactService.GetByItem(model);
            return PartialView("_PartialList", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ContactViewModel model = new ContactViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ContactTypeList = PopulateDropdown.PopulateDropdownListByTable("contact_type");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel model)
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
                        returnId = _ContactService.InsertData(model);

                        if (returnId <= 0)
                        {
                            strMessage = "Error: There was a problem while processing your request.";
                            return Content(strMessage);
                        }
                        else
                        {
                            #region Save-User
                                 UserService usrService =new UserService();
                                 UserViewModel usr = new UserViewModel();
                                 usr.LoginID = model.Email;
                                 usr.ContactID = model.ContactID;                               
                                 usr.UserName = model.ContactName;
                                 usr.UserEmail = model.Email;
                                 usr.UserPass = Password.getMd5Hash("Password@#123");
                                 usr.IsAdmin = false;
                                 usr.IsLocked = false;
                                 usr.IsChangePassword = true;
                                 usr.IsActive = true;
                                 usr.LoginUserID = model.LoginUserID;

                                 int _userId = usrService.InsertData(usr);

                                 if (_userId > 0) // if user save successfully
                                 {
                                     // get Role                                    
                                     RoleService roleService = new RoleService();
                                     RoleViewModel userRole = roleService.GetByItem(new RoleViewModel { RoleName = "Contact", ModuleId=2 }).FirstOrDefault();

                                     if (userRole.Id > 0) //Contact Role is found
                                     {
                                         UserRoleService userRoleService = new UserRoleService();
                                         int userRoleId = userRoleService.InsertData(new UserRoleViewModel { UserId = _userId, RoleId = userRole.Id });
                                     }
                                 }
                             
                               // Email Service -- Send Login ID, Password and URL to contact emial
  
                            #endregion

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
            ContactViewModel model = _ContactService.GetIById(id);
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ContactTypeList = PopulateDropdown.PopulateDropdownListByTable("contact_type");
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ContactViewModel model)
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
                        returnId = _ContactService.UpdateData(model);

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

                userId = _ContactService.DeleteData(id);

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