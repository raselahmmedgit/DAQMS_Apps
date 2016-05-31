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

        [OutputCache(Duration = 0)]
        public ActionResult GetTempLineLineChartAjax(int? companyId, int? projectId, int? deviceId, string dateRangeFrom, string dateRangeTo, string valueType, string sensor)
        {
            try
            {
                var tempSensorViewModel = new TempSensorViewModel
                {
                    CompanyId = Convert.ToInt32(companyId),
                    ProjectId = Convert.ToInt32(projectId),
                    DeviceId = Convert.ToInt32(deviceId),
                    DateRangeFrom = dateRangeFrom,
                    DateRangeTo = dateRangeTo,
                    ValueType = valueType,
                    Sensor = sensor
                };
                List<string> data = new List<string> { new string[] { "Time", "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8" }.ToString() };

                var tempSensorViewModelList = _TempSensorService.GetByItem(tempSensorViewModel).ToList();
                var dataList = tempSensorViewModelList.Select(item => new string[] { item.RecordDate.ToString(), item.T1.ToString(), item.T2.ToString(), item.T3.ToString(), item.T4.ToString(), item.T5.ToString(), item.T6.ToString(), item.T7.ToString(), item.T8.ToString() }.ToString());

                data.AddRange(dataList);

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetTempLineChartListAjax(DataTableParamModel param, int? companyId, int? projectId, int? deviceId, string dateRangeFrom, string dateRangeTo, string valueType, string sensor)
        {
            try
            {
                var tempSensorViewModel = new TempSensorViewModel
                                                          {
                                                              CompanyId = Convert.ToInt32(companyId),
                                                              ProjectId = Convert.ToInt32(projectId),
                                                              DeviceId = Convert.ToInt32(deviceId),
                                                              DateRangeFrom = dateRangeFrom,
                                                              DateRangeTo = dateRangeTo,
                                                              ValueType = valueType,
                                                              Sensor = sensor
                                                          };
                var tempSensorViewModelList = _TempSensorService.GetItemByPaging(tempSensorViewModel, param.iDisplayStart, param.iDisplayLength).ToList();

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
                             select new string[] { tempSensor.RecordDate.ToString("F"), tempSensor.T1.ToString(), tempSensor.T2.ToString(), tempSensor.T3.ToString(), tempSensor.T4.ToString(), tempSensor.T5.ToString(), tempSensor.T6.ToString(), tempSensor.T7.ToString(), tempSensor.T8.ToString() };

                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = tempSensorViewModelList.Count(),
                    iTotalDisplayRecords = filteredTempSensorViewModelList.Count(),
                    aaData = result
                },
                                JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadCompanyListAjax()
        {
            var companyList = PopulateDropdown.PopulateDropdownListByTable("Company");
            return Json(companyList, JsonRequestBehavior.AllowGet);
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