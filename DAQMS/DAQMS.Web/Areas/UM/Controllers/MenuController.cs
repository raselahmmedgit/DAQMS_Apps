using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAQMS.Core.Utility;
using DAQMS.DomainViewModel;
using DAQMS.Service;

namespace DAQMS.Web.Areas.UM.Controllers
{
    public class MenuViewModelController : BaseController
    {
        #region Global Variable Declaration
        private readonly MenuService _menuService = new MenuService();
        #endregion

        #region Constructor

        #endregion

        #region Action
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenus()
        {
            var list = _menuService.GetAll().ToList();

            //No of total records
            int totalRecords = (int)list.Count;
            //Calculate total no of page  
            int totalPages = 1;   // (int)Math.Ceiling((float)totalRecords / (float)Rows);
            var getdata = new
            {
                total = totalPages,
                page = 1,
                records = totalRecords,
                rows = (
                    from p in list
                    select new
                    {
                        cell = new string[] { 
							p.Id.ToString(),
                            p.Id.ToString(),
                            p.MenuName,
							"<a id='lnkDetailsMenu_" + p.Id + "' class='lnkAppModal btn btn-default' href='/UM/Menu/Details/" + p.Id + "'>Details</a>",
                            "<a id='lnkEditMenu_" + p.Id + "' class='lnkAppModal btn btn-default' href='/UM/Menu/Edit/" + p.Id + "'>Edit</a>",
							"<a id='lnkDeleteMenu_" + p.Id + "' class='lnkAppDelete btn btn-default' href='/UM/Menu/Delete/" + p.Id + "'>Delete</a>" }
                    }).ToArray()
            };
            return Json(getdata);
        }
        
        public PartialViewResult Details(int id)
        {
            MenuViewModel menuViewModel = _menuService.GetIById(id);
            return PartialView("_Details", menuViewModel);
        }

        public PartialViewResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult CreateAjax(MenuViewModel menuViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    menuViewModel.Id = _menuService.InsertData(menuViewModel);
                    menuViewModel.Created();
                }
                else
                {
                    menuViewModel.Message = this.GetModelStateError();
                }
                
            }
            catch (Exception ex)
            {
                menuViewModel.Message = AppConstant.Messages.UnhandelledError;
            }
            
            return Json(new { success = menuViewModel.IsSuccess, message = menuViewModel.Message });

        }

        public PartialViewResult Edit(int id)
        {
            MenuViewModel menuViewModel = _menuService.GetIById(id);
            try
            {
                if (menuViewModel != null)
                {
                    return PartialView("_Edit", menuViewModel);
                }
                else
                {
                    return PartialView("_Error", GetErrorViewModel(MessageType.warning.ToString(), AppConstant.Messages.NotFound));
                }

            }
            catch (Exception ex)
            {
                return PartialView("_Error", GetErrorViewModel(MessageType.danger.ToString(), ex.Message.ToString()));
            }
        }

        [HttpPost]
        public ActionResult EditAjax(MenuViewModel menuViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    menuViewModel.Id = _menuService.UpdateData(menuViewModel);
                    menuViewModel.Created();
                }
                else
                {
                    menuViewModel.Message = this.GetModelStateError();
                }

            }
            catch (Exception ex)
            {
                menuViewModel.Message = AppConstant.Messages.UnhandelledError;
            }

            return Json(new { success = menuViewModel.IsSuccess, message = menuViewModel.Message });
        }

        public PartialViewResult Delete(int id)
        {
            MenuViewModel menuViewModel = _menuService.GetIById(id);
            try
            {
                if (menuViewModel != null)
                {
                    return PartialView("_Delete", menuViewModel);
                }
                else
                {
                    return PartialView("_Error", GetErrorViewModel(MessageType.warning.ToString(), AppConstant.Messages.NotFound));
                }

            }
            catch (Exception ex)
            {
                return PartialView("_Error", GetErrorViewModel(MessageType.danger.ToString(), ex.Message.ToString()));
            }
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            MenuViewModel menuViewModel = _menuService.GetIById(id);

            try
            {
                if (menuViewModel != null)
                {
                    menuViewModel.Id = _menuService.DeleteData(menuViewModel.Id);
                    menuViewModel.Created();
                }
                else
                {
                    menuViewModel.Message = AppConstant.Messages.NotFound;
                }

            }
            catch (Exception ex)
            {
                menuViewModel.Message = AppConstant.Messages.UnhandelledError;
            }

            return Json(new { success = menuViewModel.IsSuccess, message = menuViewModel.Message });

        }

        #endregion
    }
}