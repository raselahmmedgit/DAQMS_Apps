using System;
using System.Collections.Generic;
using System.Data;
using DAL.Models;
using DAL.Base;
using System.Linq;
using DAQMS.Domain.Models;
using NpgsqlTypes;
using Npgsql;
using DAQMS.Domain;
using DAQMS.DomainViewModel;

namespace DAQMS.DAL
{
    public class RoleMenuDAL : DataAccessBase<RoleMenuViewModel>
    {
        public static RoleMenuDAL GetInstance()
        {
            return new RoleMenuDAL();
        }

        public override System.Int32 Save(RoleMenuViewModel item, string Mood)
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
                NpgsqlCommand command = new NpgsqlCommand("save_role_menu", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = item.Id;

                command.Parameters.Add(new NpgsqlParameter("p_role_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = item.RoleId;

                command.Parameters.Add(new NpgsqlParameter("p_menu_id", NpgsqlDbType.Integer));
                command.Parameters[2].Value = item.MenuId;

                command.Parameters.Add(new NpgsqlParameter("p_optadd", NpgsqlDbType.Bit));
                command.Parameters[3].Value = item.OptAdd;

                command.Parameters.Add(new NpgsqlParameter("p_optupdate", NpgsqlDbType.Bit));
                command.Parameters[4].Value = item.OptUpdate;

                command.Parameters.Add(new NpgsqlParameter("p_optdelete", NpgsqlDbType.Bit));
                command.Parameters[5].Value = item.OptDelete;

                command.Parameters.Add(new NpgsqlParameter("p_optview", NpgsqlDbType.Bit));
                command.Parameters[6].Value = item.OptView;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[7].Value = item.LoginUserID;

                command.Parameters.Add(new NpgsqlParameter("p_mood", NpgsqlDbType.Varchar));
                command.Parameters[8].Value = Mood;

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

        public List<RoleMenuViewModel> GetObjList(int Id,int MenuId, int ModuleId, int RoleId,
            string LoginUserID, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<RoleMenuViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_role_menu", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = Id;
                command.Parameters.Add(new NpgsqlParameter("p_menu_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = MenuId;
                command.Parameters.Add(new NpgsqlParameter("p_module_id", NpgsqlDbType.Integer));
                command.Parameters[2].Value = ModuleId;
                command.Parameters.Add(new NpgsqlParameter("p_role_id", NpgsqlDbType.Integer));
                command.Parameters[3].Value = RoleId;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = LoginUserID;
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

                // Execute the procedure and obtain a result set
                //  NpgsqlDataReader data = command.ExecuteReader();

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command))
                {
                    Adpt.Fill(dsResult);
                }

                // Fetch rows 
                results = new List<RoleMenuViewModel>();
                foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    RoleMenuViewModel obj = new RoleMenuViewModel();
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

        public override RoleMenuViewModel GetObjById(int Id)
        {
            return GetObjList(Id, 0, 0, 0, "", 1, 1).FirstOrDefault();
        }
        public override List<RoleMenuViewModel> GetObjList(RoleMenuViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.Id,item.MenuId, item.ModuleId ,item.RoleId, item.LoginUserID, startRowIndex, maxRow);
        }

        public override RoleMenuViewModel GetObjList(RoleMenuViewModel item)
        {
            return GetObjList(item.Id, item.MenuId, item.ModuleId, item.RoleId, item.LoginUserID, 0, 1).FirstOrDefault();
        }
    }
}
