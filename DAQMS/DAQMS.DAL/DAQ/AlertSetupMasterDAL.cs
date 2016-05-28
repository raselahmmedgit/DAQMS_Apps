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
    public class AlertSetupMasterDAL : DataAccessBase<AlertSetupMasterViewModel>
    {
        public static AlertSetupMasterDAL GetInstance()
        {
            return new AlertSetupMasterDAL();
        }

        public override System.Int32 Save(AlertSetupMasterViewModel item, string mood)
        {
            int id = 0;

            DataSet dsResult = new DataSet();
            NpgsqlConnection conn = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call show_cities() procedure
                NpgsqlCommand command = new NpgsqlCommand("save_alert_setup_master", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = item.Id;

                command.Parameters.Add(new NpgsqlParameter("p_contact_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = item.ContactId;

                command.Parameters.Add(new NpgsqlParameter("p_project_id", NpgsqlDbType.Integer));
                command.Parameters[2].Value = item.ProjectId;

                command.Parameters.Add(new NpgsqlParameter("p_is_email_notification", NpgsqlDbType.Bit));
                command.Parameters[3].Value = item.IsEmailNotification;

                command.Parameters.Add(new NpgsqlParameter("p_additional_email", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = item.AdditionalEmail;

                command.Parameters.Add(new NpgsqlParameter("p_is_active_temp", NpgsqlDbType.Bit));
                command.Parameters[5].Value = item.IsActiveTemp;

                command.Parameters.Add(new NpgsqlParameter("p_is_active_ctr", NpgsqlDbType.Bit));
                command.Parameters[6].Value = item.IsActiveCTR;

                command.Parameters.Add(new NpgsqlParameter("p_is_active_nodatafound", NpgsqlDbType.Bit));
                command.Parameters[7].Value = item.IsActiveNoDataFound;

                command.Parameters.Add(new NpgsqlParameter("p_nodatafound_duration", NpgsqlDbType.Integer));
                command.Parameters[8].Value = item.NoDataFoundDuration;   

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[9].Value = item.LoginUserID;

                command.Parameters.Add(new NpgsqlParameter("p_mood", NpgsqlDbType.Varchar));
                command.Parameters[10].Value = mood;

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

                // Execute the procedure and obtain a result set
                int result = (int)command.ExecuteScalar();

                if (result > 0)
                {
                    id = result;
                    tran.Commit();
                }
            }
            catch (Npgsql.NpgsqlException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                string str = "Exception : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException : " + ex.Message;
                }

                throw new Exception(str, ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }

            return id;
        }

        public List<AlertSetupMasterViewModel> GetObjList(int id, int companyId, int projectId, int contactId,
            string loginUserId, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<AlertSetupMasterViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();
            
                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_alert_setup_master", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                command.Parameters.Add(new NpgsqlParameter("p_company_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = companyId;

                command.Parameters.Add(new NpgsqlParameter("p_project_id", NpgsqlDbType.Integer));
                command.Parameters[2].Value = projectId;

                command.Parameters.Add(new NpgsqlParameter("p_contact_id", NpgsqlDbType.Integer));
                command.Parameters[3].Value = contactId;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = loginUserId;
                command.Parameters.Add(new NpgsqlParameter("p_index", NpgsqlDbType.Integer));
                command.Parameters[5].Value = startRowIndex;
                command.Parameters.Add(new NpgsqlParameter("p_maximumrows", NpgsqlDbType.Integer));
                command.Parameters[6].Value = maximumRows;

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
                command.Parameters[7].Value = "ref";

                command.ExecuteNonQuery();
                command.CommandText = "fetch all in \"ref\"";
                command.CommandType = CommandType.Text;

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command))
                {
                    Adpt.Fill(dsResult);
                }

                // Fetch rows 
                results = new List<AlertSetupMasterViewModel>();
                foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    AlertSetupMasterViewModel obj = new AlertSetupMasterViewModel();
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

        public override AlertSetupMasterViewModel GetObjById(int id)
        {
            return GetObjList(id, 0,0,0,"", 1, 1).FirstOrDefault();
        }

        public override List<AlertSetupMasterViewModel> GetObjList(AlertSetupMasterViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.Id, item.CompanyId, item.ProjectId, item.ContactId, item.LoginUserID, startRowIndex, maxRow);
        }

        public override AlertSetupMasterViewModel GetObjList(AlertSetupMasterViewModel item)
        {
            return GetObjList(item.Id, item.CompanyId, item.ProjectId, item.ContactId, item.LoginUserID, 1, 1).FirstOrDefault();
        }
    }
}


