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
    public class UserDAL : DataAccessBase<UserViewModel>
    {

        public static UserDAL GetInstance()
        {
            return new UserDAL();
        }

        public override System.Int32 Save(UserViewModel item, string Mood)
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
                NpgsqlCommand command = new NpgsqlCommand("save_users", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = item.Id;

                command.Parameters.Add(new NpgsqlParameter("p_login_id", NpgsqlDbType.Varchar));
                command.Parameters[1].Value = item.LoginID;

                command.Parameters.Add(new NpgsqlParameter("p_user_name", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = item.UserName;

                command.Parameters.Add(new NpgsqlParameter("p_user_pass", NpgsqlDbType.Varchar));
                command.Parameters[3].Value = item.UserPass;

                command.Parameters.Add(new NpgsqlParameter("p_user_email", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = item.UserEmail;

                command.Parameters.Add(new NpgsqlParameter("p_strcontact_id", NpgsqlDbType.Varchar));
                command.Parameters[5].Value = item.ContactID;

                command.Parameters.Add(new NpgsqlParameter("p_is_admin", NpgsqlDbType.Boolean));
                command.Parameters[6].Value = item.IsAdmin;

                command.Parameters.Add(new NpgsqlParameter("p_is_active", NpgsqlDbType.Boolean));
                command.Parameters[7].Value = item.IsActive;

                command.Parameters.Add(new NpgsqlParameter("p_is_locked", NpgsqlDbType.Boolean));
                command.Parameters[8].Value = item.IsLocked;

                command.Parameters.Add(new NpgsqlParameter("p_is_change_password", NpgsqlDbType.Boolean));
                command.Parameters[9].Value = item.IsChangePassword;

                command.Parameters.Add(new NpgsqlParameter("p_last_locked", NpgsqlDbType.Timestamp));
                command.Parameters[10].Value = item.LastLockedDateTime;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[11].Value = item.LoginUserID;

                command.Parameters.Add(new NpgsqlParameter("p_mood", NpgsqlDbType.Varchar));
                command.Parameters[12].Value = Mood;

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
                        else  if (param.NpgsqlDbType == NpgsqlDbType.Timestamp)
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

                if (result>0)
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


        public List<UserViewModel> GetObjList(int UserId, string LoginID, string UserName, string UserEmail, string ContactID,
           string LoginUserID, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<UserViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_users", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = UserId;
                command.Parameters.Add(new NpgsqlParameter("p_login_id", NpgsqlDbType.Varchar));
                command.Parameters[1].Value = LoginID;
                command.Parameters.Add(new NpgsqlParameter("p_user_name", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = UserName;
                command.Parameters.Add(new NpgsqlParameter("p_user_email", NpgsqlDbType.Varchar));
                command.Parameters[3].Value = UserEmail;

                command.Parameters.Add(new NpgsqlParameter("p_strcontact_id", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = ContactID;
                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[5].Value = LoginUserID;
                command.Parameters.Add(new NpgsqlParameter("p_index", NpgsqlDbType.Integer));
                command.Parameters[6].Value = startRowIndex;
                command.Parameters.Add(new NpgsqlParameter("p_maximumrows", NpgsqlDbType.Integer));
                command.Parameters[7].Value = maximumRows;

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
                command.Parameters[8].Value = "ref";

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
                 results = new List<UserViewModel>();
                 foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    UserViewModel obj = new UserViewModel();
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

        public override UserViewModel GetObjById(int Id)
        {
            return GetObjList(Id, "", "", "", "", "", 1, 1).FirstOrDefault();
        }
        public override List<UserViewModel> GetObjList(UserViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.Id, item.LoginID, item.UserName, item.UserEmail, item.ContactID, item.LoginUserID, startRowIndex, maxRow);
        }

        public override UserViewModel GetObjList(UserViewModel item)
        {
            return GetObjList(item.Id, item.LoginID, item.UserName, item.UserEmail, item.ContactID, item.LoginUserID, 0, 1).FirstOrDefault();
        }
    }
}
