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
    public class UserRightDAL : DataAccessBase<UserRightViewModel>
    {

        public static UserRightDAL GetInstance()
        {
            return new UserRightDAL();
        }

        public override System.Int32 Save(UserRightViewModel item, string mood)
        {
            throw new NotImplementedException();

        }

        public List<UserRightViewModel> GetObjList(int moduleId, int userId, string loginUserId, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<UserRightViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_user_right", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_module_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = moduleId;
                command.Parameters.Add(new NpgsqlParameter("p_userid", NpgsqlDbType.Integer));
                command.Parameters[1].Value = userId;
               
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
                 results = new List<UserRightViewModel>();
                 foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    UserRightViewModel obj = new UserRightViewModel();
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

        public override UserRightViewModel GetObjById(int UserId)
        {
            return GetObjList(0, UserId, "", 1, 1).FirstOrDefault();
        }

        public override List<UserRightViewModel> GetObjList(UserRightViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.ModuleId, item.UserId, item.LoginUserID, startRowIndex, maxRow);
        }

        public override UserRightViewModel GetObjList(UserRightViewModel item)
        {
            return GetObjList(item.ModuleId, item.UserId, item.LoginUserID, 1, 1).FirstOrDefault();
        }
    }
}