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
    public class LoginHistoryDAL : DataAccessBase<LoginHistoryViewModel>
    {
        public static LoginHistoryDAL GetInstance()
        {
            return new LoginHistoryDAL();
        }

        public override System.Int32 Save(LoginHistoryViewModel item, string Mood)
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
                NpgsqlCommand command = new NpgsqlCommand("save_login_history", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = item.Id;

                command.Parameters.Add(new NpgsqlParameter("p_userid", NpgsqlDbType.Integer));
                command.Parameters[1].Value = item.UserId;

                command.Parameters.Add(new NpgsqlParameter("p_user_id", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = item.LoginUserID;

                command.Parameters.Add(new NpgsqlParameter("p_mood", NpgsqlDbType.Varchar));
                command.Parameters[3].Value = Mood;

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

        public List<LoginHistoryViewModel> GetObjList(int Id, string LoginUserID, int startRowIndex, int maximumRows)
        {

            List<LoginHistoryViewModel> results = null;

            return results;
        }

        public override LoginHistoryViewModel GetObjById(int Id)
        {
            return GetObjList(Id, "", 1, 1).FirstOrDefault();
        }
        public override List<LoginHistoryViewModel> GetObjList(LoginHistoryViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.Id, item.LoginUserID, startRowIndex, maxRow);
        }

        public override LoginHistoryViewModel GetObjList(LoginHistoryViewModel item)
        {
            return GetObjList(item.Id, item.LoginUserID, 0, 1).FirstOrDefault();
        }
    }
}
