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
    public class RoleDAL : DataAccessBase<RoleViewModel>
    {
        public static RoleDAL GetInstance()
        {
            return new RoleDAL();
        }

        public override System.Int32 Save(RoleViewModel item, string mood)
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
                NpgsqlCommand command = new NpgsqlCommand("save_roles", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = item.Id;

                command.Parameters.Add(new NpgsqlParameter("p_module_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = item.ModuleId;

                command.Parameters.Add(new NpgsqlParameter("p_role_name", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = item.RoleName;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[3].Value = item.LoginUserID;

                command.Parameters.Add(new NpgsqlParameter("p_mood", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = mood;

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

        public List<RoleViewModel> GetObjList(int id, int moduleId, string loginUserId, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<RoleViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_roles", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;
                command.Parameters.Add(new NpgsqlParameter("p_module_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = moduleId;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = loginUserId;
                command.Parameters.Add(new NpgsqlParameter("p_index", NpgsqlDbType.Integer));
                command.Parameters[3].Value = startRowIndex;
                command.Parameters.Add(new NpgsqlParameter("p_maximumrows", NpgsqlDbType.Integer));
                command.Parameters[4].Value = maximumRows;

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
                command.Parameters[5].Value = "ref";

                command.ExecuteNonQuery();
                command.CommandText = "fetch all in \"ref\"";
                command.CommandType = CommandType.Text;

                // Execute the procedure and obtain a result set
                //  NpgsqlDataReader data = command.ExecuteReader();

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command))
                {
                    Adpt.Fill(dsResult);
                }

                // Fetch rows 
                results = new List<RoleViewModel>();
                foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    RoleViewModel obj = new RoleViewModel();
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

        public override RoleViewModel GetObjById(int id)
        {
            return GetObjList(id, 0, "", 1, 1).FirstOrDefault();
        }
        
        public override List<RoleViewModel> GetObjList(RoleViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.Id, item.ModuleId, item.LoginUserID, startRowIndex, maxRow);
        }

        public override RoleViewModel GetObjList(RoleViewModel item)
        {
            return GetObjList(item.Id, item.ModuleId, item.LoginUserID, 0, 1).FirstOrDefault();
        }
    }
}
