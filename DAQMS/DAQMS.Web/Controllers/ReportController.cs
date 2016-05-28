using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAQMS.DomainViewModel;
using DAQMS.Service;
using DAQMS.Web.Helper;

namespace DAQMS.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly TempSensorService _TempSensorService;

        public ReportController()
        {
            this._TempSensorService = new TempSensorService();
        }

        [HttpGet]
        public ActionResult TempLineChart()
        {
            TempSensorViewModel model = new TempSensorViewModel();
            model.CompanyList = PopulateDropdown.PopulateDropdownListByTable("Company");
            //model.ProjectList = PopulateDropdown.PopulateDropdownListByTable("Project");
            //model.DeviceList = PopulateDropdown.PopulateDropdownListByTable("Device");
            model.ValueTypeList = PopulateDropdown.PopulateDropdownValueTypeList();
            model.SensorList = PopulateDropdown.PopulateDropdownSensorList();
            return View(model);
        }

        public ActionResult GetTempLineList(DataTableParamModel param, int CompanyId, int ProjectId, int DeviceId, )
        {
            var tempSensorViewModelList = _TempSensorService.GetItemByPaging(new TempSensorViewModel(), param.iDisplayStart, param.iDisplayLength).ToList();

            IEnumerable<TempSensorViewModel> filteredTempSensorViewModelList;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredTempSensorViewModelList = tempSensorViewModelList.Where(cat => (cat.CompanyName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredTempSensorViewModelList = tempSensorViewModelList;
            }

            var viewOdjects = filteredTempSensorViewModelList.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from tempSensor in viewOdjects
                         select new[] { tempSensor.RecordDate, tempSensor.T1, tempSensor.T2, tempSensor.T3, tempSensor.T4, tempSensor.T5, tempSensor.T6, tempSensor.T7, tempSensor.T8 };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tempSensorViewModelList.Count(),
                iTotalDisplayRecords = filteredTempSensorViewModelList.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadProjectListByCompanyIdAjax(int companyId)
        {
            var projectList = PopulateDropdown.PopulateDropdownListByTable("Project", companyId);
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadDeviceListByCompanyIdAjax(int projectId)
        {
            var deviceList = PopulateDropdown.PopulateDropdownListByTable("Device", projectId);
            return Json(deviceList, JsonRequestBehavior.AllowGet);
        }
    }
}