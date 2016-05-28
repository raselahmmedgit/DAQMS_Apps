using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAQMS.DAL.Base;
using DAQMS.DAL.Models;
using NpgsqlTypes;
using Npgsql;
using DAQMS.Domain;
using DAQMS.DomainViewModel;

namespace DAQMS.DAL
{
    public class CommonDAL
    {
        public static CommonDAL GetInstance()
        {
            return new CommonDAL();
        }

        public List<CommonViewModel> GetObjList(string TableType, int Id, string Name, int RefId=0)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            string sql = string.Empty;

            List<CommonViewModel> results = null;

            TableType = TableType.ToLower();

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                //Find SQL
                if (TableType == "alert_type")
                {
                    sql = "SELECT * FROM (SELECT id, alert_type as name  FROM alert_type) abc WHERE 0=0";
                }
                else if (TableType == "contact_type")
                {
                    sql = "SELECT * FROM (SELECT id, type_name as name  FROM contact_type) abc WHERE 0=0";
                }
                else if (TableType == "device_status")
                {
                    sql = "SELECT * FROM (SELECT id, status_name as name  FROM device_status) abc WHERE 0=0";
                }
                else if (TableType == "project_status")
                {
                    sql = "SELECT * FROM (SELECT id, status_name as name  FROM project_status) abc WHERE 0=0";
                }
                else if (TableType == "company")
                {
                    sql = "SELECT * FROM (SELECT id, company_name as name  FROM company) abc WHERE 0=0";
                }
                else if (TableType == "relay_state")
                {
                    sql = "SELECT * FROM (SELECT id, relay_state as name  FROM relay_state) abc WHERE 0=0";
                }
                else if (TableType == "modules")
                {
                    sql = "SELECT * FROM (SELECT id, module_name as name  FROM modules) abc WHERE 0=0";
                }
                else if (TableType == "project")
                {
                    sql = "SELECT * FROM (SELECT id, project_name as name  FROM project WHERE company_id="+ RefId +") abc WHERE 0=0";
                }
                else if (TableType == "devices")
                {
                    sql = "SELECT * FROM (SELECT id, strdevice_id as name  FROM devices WHERE project_id=" + RefId + ") abc WHERE 0=0";
                }
                else if (TableType == "contact")
                {
                    sql = "SELECT * FROM (SELECT id, contact_name as name  FROM contact WHERE company_id=" + RefId + ") abc WHERE 0=0";
                }


                if (Id > 0)
                {
                    sql += " AND id=" + Id;
                }

                if (!(System.String.IsNullOrEmpty(Name)))
                {
                    sql += " AND name='" + Name + "'";
                }

                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                command.CommandType = CommandType.Text;


                //command.ExecuteNonQuery();
                //command.CommandText = "fetch all in \"ref\"";
                //command.CommandType = CommandType.Text;

                //// Execute the procedure and obtain a result set
                  NpgsqlDataReader data = command.ExecuteReader();

                 
                // Fetch rows 
                results = new List<CommonViewModel>();

                CommonViewModel selectObj = new CommonViewModel();
                results.Add(selectObj);

                while (data.Read())
                {
                    CommonViewModel obj = new CommonViewModel();
                    obj.Id = (int)data[0];
                    obj.Name = data[1].ToString();
                    results.Add(obj);
  
                }
              
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }
            return results;
        }
    }
}
