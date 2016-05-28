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
    public class TempSensorDAL : DataAccessBase<TempSensorViewModel>
    {
        public static TempSensorDAL GetInstance()
        {
            return new TempSensorDAL();
        }

        public override System.Int32 Save(TempSensorViewModel item, string mood)
        {
            return -1;
        }

        public List<TempSensorViewModel> GetObjList(int companyId, int projectId, int deviceId, string fromDate,  string toDate, string valueType,
            string loginUserId, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<TempSensorViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();
            
                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_temp_sensor_data", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_company_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = companyId;

                command.Parameters.Add(new NpgsqlParameter("p_project_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = projectId;

                command.Parameters.Add(new NpgsqlParameter("p_device_id", NpgsqlDbType.Integer));
                command.Parameters[2].Value = deviceId;

                command.Parameters.Add(new NpgsqlParameter("p_from_timestamp", NpgsqlDbType.Varchar));
                command.Parameters[3].Value = fromDate;

                command.Parameters.Add(new NpgsqlParameter("p_to_timestamp", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = toDate;

                command.Parameters.Add(new NpgsqlParameter("p_temp_sensor_val_type", NpgsqlDbType.Varchar));
                command.Parameters[5].Value = valueType;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[6].Value = loginUserId;
                command.Parameters.Add(new NpgsqlParameter("p_index", NpgsqlDbType.Integer));
                command.Parameters[7].Value = startRowIndex;
                command.Parameters.Add(new NpgsqlParameter("p_maximumrows", NpgsqlDbType.Integer));
                command.Parameters[8].Value = maximumRows;

                // reset parameter by default value
                #region param reset
                foreach (NpgsqlParameter param in command.Parameters)
                {
                    //Manage parameter value
                    if (param.Value == null)
                    {
                        if (param.NpgsqlDbType == NpgsqlDbType.Varchar)
                        {
                            param.Value = "";
                        }
                        else if (param.NpgsqlDbType == NpgsqlDbType.Timestamp)
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                    else if (param.NpgsqlDbType == NpgsqlDbType.Timestamp)
                    {
                        if (Convert.ToDateTime(param.Value) == DateTime.MinValue || Convert.ToDateTime(param.Value) == DateTime.MaxValue)
                        {
                            param.Value = DBNull.Value;
                        }
                    }
                }
                #endregion  param reset

                command.Parameters.Add(new NpgsqlParameter("ref", NpgsqlDbType.Refcursor));
                command.Parameters[9].Value = "ref";

                command.ExecuteNonQuery();
                command.CommandText = "fetch all in \"ref\"";
                command.CommandType = CommandType.Text;

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command))
                {
                    Adpt.Fill(dsResult);
                }

                // Fetch rows 
                results = new List<TempSensorViewModel>();
                foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    TempSensorViewModel obj = new TempSensorViewModel();
                    ModelMapperBase.GetInstance().MapItem(obj, dr);
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

        public override TempSensorViewModel GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public override List<TempSensorViewModel> GetObjList(TempSensorViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.CompanyId, item.ProjectId, item.DeviceId, item.DateRangeFrom,item.DateRangeTo, item.ValueType, item.LoginUserID, startRowIndex, maxRow);
        }

        public override TempSensorViewModel GetObjList(TempSensorViewModel item)
        {
            return GetObjList(item.CompanyId, item.ProjectId, item.DeviceId, item.DateRangeFrom, item.DateRangeTo, item.ValueType, item.LoginUserID, 1, 1).FirstOrDefault();
        }
    }
}

