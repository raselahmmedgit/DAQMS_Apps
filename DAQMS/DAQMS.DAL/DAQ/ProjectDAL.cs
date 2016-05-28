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
    public class ProjectDAL : DataAccessBase<ProjectViewModel>
    {
        public static ProjectDAL GetInstance()
        {
            return new ProjectDAL();
        }

        public override System.Int32 Save(ProjectViewModel item, string mood)
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
                NpgsqlCommand command = new NpgsqlCommand("save_project", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = item.Id;

                
                command.Parameters.Add(new NpgsqlParameter("p_company_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = item.CompanyId;

                command.Parameters.Add(new NpgsqlParameter("p_project_name", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = item.ProjectName;

                command.Parameters.Add(new NpgsqlParameter("p_address", NpgsqlDbType.Varchar));
                command.Parameters[3].Value = item.Address;

                command.Parameters.Add(new NpgsqlParameter("p_city", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = item.City;

                command.Parameters.Add(new NpgsqlParameter("p_state", NpgsqlDbType.Varchar));
                command.Parameters[5].Value = item.State;

                command.Parameters.Add(new NpgsqlParameter("p_zip", NpgsqlDbType.Varchar));
                command.Parameters[6].Value = item.Zip;

                command.Parameters.Add(new NpgsqlParameter("p_number_of_unit", NpgsqlDbType.Integer));
                command.Parameters[7].Value = item.NumberOfUnit;

                command.Parameters.Add(new NpgsqlParameter("p_monitoring_rate", NpgsqlDbType.Double));
                command.Parameters[8].Value = item.MonitoringRate;

                command.Parameters.Add(new NpgsqlParameter("p_project_status_id", NpgsqlDbType.Integer));
                command.Parameters[9].Value = item.ProjectStatusId;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[10].Value = item.LoginUserID;

                command.Parameters.Add(new NpgsqlParameter("p_mood", NpgsqlDbType.Varchar));
                command.Parameters[11].Value = mood;

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

        public List<ProjectViewModel> GetObjList(int id, int companyId, string projectName, int projectStatusId, 
            string loginUserId, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<ProjectViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_project", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                command.Parameters.Add(new NpgsqlParameter("p_company_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = companyId;

                command.Parameters.Add(new NpgsqlParameter("p_project_name", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = projectName;

                command.Parameters.Add(new NpgsqlParameter("p_project_status_id", NpgsqlDbType.Integer));
                command.Parameters[3].Value = projectStatusId;

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
                results = new List<ProjectViewModel>();
                foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    ProjectViewModel obj = new ProjectViewModel();
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

        public override ProjectViewModel GetObjById(int id)
        {
            return GetObjList(id, 0,"",0,"", 1, 1).FirstOrDefault();
        }

        public override List<ProjectViewModel> GetObjList(ProjectViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.Id, item.CompanyId,item.ProjectName,item.ProjectStatusId, item.LoginUserID, startRowIndex, maxRow);
        }

        public override ProjectViewModel GetObjList(ProjectViewModel item)
        {
            return GetObjList(item.Id, item.CompanyId, item.ProjectName, item.ProjectStatusId, item.LoginUserID, 1, 1).FirstOrDefault();
        }
    }
}

