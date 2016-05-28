using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;


namespace Code
{
    public static class Common
    {
        // a 32 character hexadecimal string.
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
                //sBuilder.Append(Convert.ToString(data[i], 2).PadLeft(8, '0')); //Convert into binary
            }

            // Return the hexadecimal string.
            return sBuilder.ToString().ToLower();
        }

        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static SelectList YearUptoCurrentList(int StartYear, int EndYear, int? DefaultYear)
        {
            SelectList _YearList;
            List<SelectListItem> itemList = new List<SelectListItem>();

            int year = StartYear;

            while (EndYear >= year)
            {
                SelectListItem item = new SelectListItem();
                item.Value = item.Text = year.ToString();
                if (DefaultYear != null && year == (int)DefaultYear)
                {
                    item.Selected = true;
                }
                itemList.Add(item);
                year++;
            }

            _YearList = new SelectList(itemList, "Value", "Text", DefaultYear);
            return _YearList;
        }
        public static SelectList CalenderYearList()
        {
            int StartYear = 1971;
            int EndYear = DateTime.Now.Year + 2;
            int? DefaultYear = DateTime.Now.Year;
            SelectList _YearList;
            List<SelectListItem> itemList = new List<SelectListItem>();

            int year = StartYear;

            for (int i = year; i <= EndYear; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Value = item.Text = i.ToString();
                itemList.Add(item);
            }

            _YearList = new SelectList(itemList.OrderByDescending(x => x.Value).ToList(), "Value", "Text");
            return _YearList;
        }

        public static List<SelectListItem> FinancialYearList()
        {
            int startYear = 2010;
            int EndYear = DateTime.Now.Year + 5;
            int? DefaultYear = DateTime.Now.Year;
            List<SelectListItem> itemList = new List<SelectListItem>();

            for (int i = startYear; i <= EndYear; i++)
            {
                itemList.Add(new SelectListItem { Text = i.ToString() + "-" + (i + 1).ToString(), Value = i.ToString() + "-" + (i + 1).ToString() });
            }
            return itemList.OrderByDescending(x => x.Value).ToList();
        }

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

                return items.OrderBy(s => s.Text).OrderByDescending(x => x.Text).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetModelStateErrorMessage(ModelStateDictionary modelStateDictionary)
        {
            string message = @"<div id='ErrorMessage'>";

            foreach (var modelStateValues in modelStateDictionary.Values)
            {
                if (modelStateValues.Errors.Any())
                {
                    foreach (var modelError in modelStateValues.Errors)
                    {
                        message += @"<p>";
                        message += modelError.ErrorMessage;
                        message += "</p>";
                    }
                }
            }

            message += "</div>";

            return message;
        }


        public static IList<SelectListItem> GetQueueNoList()
        {
            IList<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "Select", Value = "0" });
            list.Add(new SelectListItem { Text = "Queue-1", Value = "1" });
            list.Add(new SelectListItem { Text = "Queue-2", Value = "2" });
            list.Add(new SelectListItem { Text = "Queue-3", Value = "3" });

            return list;
        }

        public static IList<SelectListItem> GetGenderList()
        {
            IList<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "Select", Value = "" });
            list.Add(new SelectListItem { Text = "Male", Value = "M" });
            list.Add(new SelectListItem { Text = "Female", Value = "F" });

            return list;
        }

    }
}