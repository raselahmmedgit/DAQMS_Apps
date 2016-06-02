using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAQMS.DomainViewModel;
using DAQMS.Service;

namespace DAQMS.Web.Helper
{
    public static class PopulateDropdown
    {
        //Populate Dropdownlist
        public static List<SelectListItem> PopulateDropdownList<T>(this List<T> objectList, string valueField, string textField)
        {
            try
            {
                var selectedList = new SelectList(objectList, valueField, textField);
                List<SelectListItem> items;
                IEnumerable<SelectListItem> listOfItems;
                listOfItems = from obj in selectedList select new SelectListItem { Selected = false, Text = obj.Text, Value = obj.Value };
                items = listOfItems.ToList();

                return items.OrderBy(s => s.Text).OrderBy(x => x.Text).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<SelectListItem> PopulateDropdownListByTable(string tableName, int RefId = 0)
        {
            string valueField = "Id";
            string textField = "Name";

            List<CommonViewModel> objectList = new List<CommonViewModel>();
            DropDownService data = new DropDownService();
            try
            {
                objectList = data.GetItemByTableName(tableName, RefId);

                var selectedList = new SelectList(objectList, valueField, textField);
                List<SelectListItem> items;
                IEnumerable<SelectListItem> listOfItems;
                listOfItems = from obj in selectedList select new SelectListItem { Selected = true, Text = obj.Text, Value = obj.Value };
                items = listOfItems.ToList();

                return items.OrderBy(s => s.Text).OrderBy(x => x.Text).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static List<SelectListItem> PopulateDropdownValueTypeList()
        {
            List<SelectListItem> objectList = new List<SelectListItem>();
            try
            {

                objectList.Add(new SelectListItem() { Selected = true, Text = "Min", Value = "1" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "Avg", Value = "2" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "Max", Value = "3" });
                return objectList.OrderBy(s => s.Text).OrderBy(x => x.Text).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static List<SelectListItem> PopulateDropdownSensorList()
        {
            List<SelectListItem> objectList = new List<SelectListItem>();
            try
            {

                objectList.Add(new SelectListItem() { Selected = true, Text = "T1", Value = "1" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "T2", Value = "2" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "T3", Value = "3" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "T4", Value = "4" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "T5", Value = "5" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "T6", Value = "6" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "T7", Value = "7" });
                objectList.Add(new SelectListItem() { Selected = false, Text = "T8", Value = "8" });
                return objectList.OrderBy(s => s.Text).OrderBy(x => x.Text).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}