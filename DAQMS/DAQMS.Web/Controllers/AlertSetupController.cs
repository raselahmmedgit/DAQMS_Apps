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
    public class AlertSetupController : Controller
    {
        private readonly ContactService _ContactService;
        private readonly ProjectService _ProjectService;
        private readonly ProjectContactService _ProjectContactService;
        private readonly AlertSetupMasterService _AlertSetupMasterService;
        private readonly AlertSetupTempService _AlertSetupTempService;
        private readonly AlertSetupCTRService _AlertSetupCTRService;

        public AlertSetupController()
        {
            this._ContactService = new ContactService();
            this._ProjectService = new ProjectService();
            this._ProjectContactService = new ProjectContactService();
            this._AlertSetupMasterService = new AlertSetupMasterService();
            this._AlertSetupTempService = new AlertSetupTempService();
            this._AlertSetupCTRService = new AlertSetupCTRService();
        }

        public ActionResult Index()
        {
            AlertSetupMasterViewModel model = new AlertSetupMasterViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("Project");
            model.ContactNameList = PopulateDropdown.PopulateDropdownListByTable("contact");
            return View(model);
        }

        public PartialViewResult List(int? page)
        {
            IList<AlertSetupMasterViewModel> model = _AlertSetupMasterService.GetAll();
            return PartialView("_PartialList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult List(int? page, AlertSetupMasterViewModel model)
        {
            IList<AlertSetupMasterViewModel> data = _AlertSetupMasterService.GetByItem(model);
            return PartialView("_PartialList", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AlertSetupMasterViewModel model = new AlertSetupMasterViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("Project");
            model.ContactNameList = PopulateDropdown.PopulateDropdownListByTable("contact");

            SensorViewModel sensorModel = new SensorViewModel();
            SensorService sensorService = new SensorService();
            sensorModel.TableName = "sensor_temp";
            List<SensorViewModel> lst = sensorService.GetByItem(sensorModel);

            model.AlertSetupTempList = new List<AlertSetupTempViewModel>();

            if (lst.Count() > 0)
            {
                foreach (var item in lst)
                {
                    AlertSetupTempViewModel obj = new AlertSetupTempViewModel();
                    obj.SensorId = item.Id;
                    obj.SensorName = item.SensorName;
                    obj.AlertSetupId = model.Id;
                    obj.LoginUserID = model.LoginUserID;
                    obj.LowTemp = 0;
                    obj.HighTemp = 0;
                    model.AlertSetupTempList.Add(obj);
                }
            }

            sensorModel.TableName = "sensor_ct";
            lst = sensorService.GetByItem(sensorModel);

            model.AlertSetupCTRList = new List<AlertSetupCTRViewModel>();

            if (lst.Count() > 0)
            {
                foreach (var item in lst)
                {
                    AlertSetupCTRViewModel obj = new AlertSetupCTRViewModel();
                    obj.SensorId = item.Id;
                    obj.SensorName = item.SensorName;
                    obj.AlertSetupId = model.Id;
                    obj.LoginUserID = model.LoginUserID;
                    obj.MaxVal = 0;
                    obj.LengthOfTime = 0;
                    model.AlertSetupCTRList.Add(obj);
                }
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(AlertSetupMasterViewModel model)
        {
            int returnId = -1;
            string strMessage = string.Empty;
            model.LoginUserID = HttpContext.User.Identity.Name;
            strMessage = ValidateBusinessLogic(model);

            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(strMessage))
                    {
                        returnId = _AlertSetupMasterService.InsertData(model);

                        if (returnId <= 0)
                        {
                            strMessage = "Error: There was a problem while processing your request.";
                            return Content(strMessage);
                        }
                        else
                        {

                            #region Alert Setup Tempperature

                            List<AlertSetupTempViewModel> alertSetupTempList = model.AlertSetupTempList.Where(x => x.IsActive == true).ToList();

                            if (alertSetupTempList.Count() > 0)
                            {
                                foreach (var item in alertSetupTempList)
                                {
                                    item.AlertSetupId = returnId;
                                    item.LoginUserID = HttpContext.User.Identity.Name;
                                    _AlertSetupTempService.InsertData(item);
                                }
                            }

                            #endregion

                            #region Alert Setup CTR

                            List<AlertSetupCTRViewModel> alertSetupCTRList = model.AlertSetupCTRList.Where(x => x.IsActive == true).ToList();

                            if (alertSetupCTRList.Count() > 0)
                            {
                                foreach (var item in alertSetupCTRList)
                                {
                                    item.AlertSetupId = returnId;
                                    item.LoginUserID = HttpContext.User.Identity.Name;
                                    _AlertSetupCTRService.InsertData(item);
                                }
                            }

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
            AlertSetupMasterViewModel model = _AlertSetupMasterService.GetIById(id);
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("company");
            model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("project", model.CompanyId);
            model.ContactNameList = PopulateDropdown.PopulateDropdownListByTable("contact", model.CompanyId);

            SensorViewModel sensorModel = new SensorViewModel();
            SensorService sensorService = new SensorService();
            sensorModel.TableName = "sensor_temp";
            List<SensorViewModel> lst = sensorService.GetByItem(sensorModel);

            model.AlertSetupTempList = new List<AlertSetupTempViewModel>();

            if (lst.Count() > 0)
            {
                AlertSetupTempViewModel Tempobj = new AlertSetupTempViewModel();
                Tempobj.AlertSetupId = model.Id;

                List<AlertSetupTempViewModel> lstTempDB = new List<AlertSetupTempViewModel>();
                lstTempDB = _AlertSetupTempService.GetByItem(Tempobj);

                foreach (var item in lst)
                {
                    AlertSetupTempViewModel obj = new AlertSetupTempViewModel();
                    obj.SensorId = item.Id;
                    obj.SensorName = item.SensorName;
                    obj.AlertSetupId = model.Id;
                    obj.LoginUserID = model.LoginUserID;
                    var assignedLst = (from tr in lstTempDB where tr.SensorId == item.Id select tr).FirstOrDefault();
                    obj.LowTemp = assignedLst != null ? assignedLst.LowTemp : 0;
                    obj.HighTemp = assignedLst != null ? assignedLst.HighTemp : 0;
                    obj.IsActive = assignedLst != null ? assignedLst.IsActive : false;

                    model.AlertSetupTempList.Add(obj);
                }
            }

            sensorModel.TableName = "sensor_ct";
            lst = sensorService.GetByItem(sensorModel);
            model.AlertSetupCTRList = new List<AlertSetupCTRViewModel>();

            if (lst.Count() > 0)
            {
                AlertSetupCTRViewModel objCTR = new AlertSetupCTRViewModel();
                objCTR.AlertSetupId = model.Id;
                List<AlertSetupCTRViewModel> objAltCTRList = new List<AlertSetupCTRViewModel>();
                objAltCTRList = _AlertSetupCTRService.GetByItem(objCTR);

                foreach (var item in lst)
                {
                    var assignCTR = (from tr in objAltCTRList where tr.SensorId == item.Id select tr).LastOrDefault();
                    AlertSetupCTRViewModel obj = new AlertSetupCTRViewModel();
                    obj.SensorId = item.Id;
                    obj.SensorName = item.SensorName;
                    obj.AlertSetupId = model.Id;
                    obj.LoginUserID = model.LoginUserID;
                    obj.MaxVal = assignCTR != null ? assignCTR.MaxVal : 0;
                    obj.LengthOfTime = assignCTR != null ? assignCTR.LengthOfTime : 0;
                    obj.IsActive = assignCTR != null ? assignCTR.IsActive : false;
                    model.AlertSetupCTRList.Add(obj);
                }
            }



            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AlertSetupMasterViewModel model)
        {
            int returnId = -1;
            string strMessage = string.Empty;
            model.LoginUserID = HttpContext.User.Identity.Name;
            strMessage = ValidateBusinessLogic(model);

            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(strMessage))
                    {
                        returnId = _AlertSetupMasterService.UpdateData(model);

                        if (returnId <= 0)
                        {
                            strMessage = "Error: There was a problem while processing your request.";
                            return Content(strMessage);
                        }
                        else
                        {
                            #region Alert Setup Tempperature

                            List<AlertSetupTempViewModel> alertSetupTempList = model.AlertSetupTempList.Where(x => x.IsActive == true).ToList();

                            if (alertSetupTempList.Count() > 0)
                            {
                                foreach (var item in alertSetupTempList)
                                {
                                    item.AlertSetupId = returnId;
                                    item.LoginUserID = HttpContext.User.Identity.Name;
                                    _AlertSetupTempService.InsertData(item);
                                }
                            }

                            #endregion

                            #region Alert Setup CTR

                            List<AlertSetupCTRViewModel> alertSetupCTRList = model.AlertSetupCTRList.Where(x => x.IsActive == true).ToList();

                            if (alertSetupCTRList.Count() > 0)
                            {
                                foreach (var item in alertSetupCTRList)
                                {
                                    item.AlertSetupId = returnId;
                                    item.LoginUserID = HttpContext.User.Identity.Name;
                                    _AlertSetupCTRService.InsertData(item);
                                }
                            }

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

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            int userId = -1;
            bool result = true;
            string errMsg = string.Empty;
            try
            {

                userId = _AlertSetupMasterService.DeleteData(id);

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

        private string ValidateBusinessLogic(AlertSetupMasterViewModel model)
        {
            string msg = string.Empty;
            List<AlertSetupTempViewModel> alertTempList = model.AlertSetupTempList.Where(x => x.IsActive == true).ToList();
            if (alertTempList.Count == 0)
            {
                return msg = "You must select at least one temperature sensor.";
            }
            List<AlertSetupCTRViewModel> alertCTRList = model.AlertSetupCTRList.Where(x => x.IsActive == true).ToList();
            if (alertCTRList.Count == 0)
            {
                return msg = "You must select at least one CTR sensor.";
            }
            return msg;
        }
    }
}