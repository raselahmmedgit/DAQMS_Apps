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
    public class ContactDAL : DataAccessBase<ContactViewModel>
    {
        public static ContactDAL GetInstance()
        {
            return new ContactDAL();
        }

        public override System.Int32 Save(ContactViewModel item, string mood)
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
                NpgsqlCommand command = new NpgsqlCommand("save_contact", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = item.Id;

                command.Parameters.Add(new NpgsqlParameter("p_strcontact_id", NpgsqlDbType.Varchar));
                command.Parameters[1].Value = item.ContactID;

                command.Parameters.Add(new NpgsqlParameter("p_contact_name", NpgsqlDbType.Varchar));
                command.Parameters[2].Value = item.ContactName;

                command.Parameters.Add(new NpgsqlParameter("p_contact_type_id", NpgsqlDbType.Integer));
                command.Parameters[3].Value = item.ContactTypeId;

                command.Parameters.Add(new NpgsqlParameter("p_company_id", NpgsqlDbType.Integer));
                command.Parameters[4].Value = item.CompanyId;

                command.Parameters.Add(new NpgsqlParameter("p_notes", NpgsqlDbType.Varchar));
                command.Parameters[5].Value = item.Notes;

                command.Parameters.Add(new NpgsqlParameter("p_workphone", NpgsqlDbType.Varchar));
                command.Parameters[6].Value = item.WorkPhone;

                command.Parameters.Add(new NpgsqlParameter("p_cellphone", NpgsqlDbType.Varchar));
                command.Parameters[7].Value = item.CellPhone;

                command.Parameters.Add(new NpgsqlParameter("p_email", NpgsqlDbType.Varchar));
                command.Parameters[8].Value = item.Email;

                command.Parameters.Add(new NpgsqlParameter("p_is_create_user", NpgsqlDbType.Boolean));
                command.Parameters[9].Value = item.IsCreateUser;

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

        public List<ContactViewModel> GetObjList(int id, int companyId, int projectId, string projectName, string contactID, string contactName, 
            string loginUserId, int startRowIndex, int maximumRows)
        {
            DataSet dsResult = new DataSet();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = null;

            List<ContactViewModel> results = null;

            try
            {
                conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
                conn.Open();

                // p_id integer, p_company_id integer, p_project_id integer, p_project_name character varying, 
                //p_strcontact_id character varying, p_contact_name character varying, 

                // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
                NpgsqlTransaction tran = conn.BeginTransaction();

                // Define a command to call procedure
                NpgsqlCommand command = new NpgsqlCommand("get_contact", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_id", NpgsqlDbType.Integer));
                command.Parameters[0].Value = id;

                command.Parameters.Add(new NpgsqlParameter("p_company_id", NpgsqlDbType.Integer));
                command.Parameters[1].Value = companyId;

                command.Parameters.Add(new NpgsqlParameter("p_project_id", NpgsqlDbType.Integer));
                command.Parameters[2].Value = projectId;

                command.Parameters.Add(new NpgsqlParameter("p_project_name", NpgsqlDbType.Varchar));
                command.Parameters[3].Value = projectName;

                command.Parameters.Add(new NpgsqlParameter("p_strcontact_id", NpgsqlDbType.Varchar));
                command.Parameters[4].Value = contactID;

                command.Parameters.Add(new NpgsqlParameter("p_contact_name", NpgsqlDbType.Varchar));
                command.Parameters[5].Value = contactName;

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
                results = new List<ContactViewModel>();
                foreach (DataRow dr in dsResult.Tables[0].Rows)
                {
                    ContactViewModel obj = new ContactViewModel();
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

        public override ContactViewModel GetObjById(int id)
        {
            return GetObjList(id, 0, 0,"","","","", 1, 1).FirstOrDefault();
        }

        public override List<ContactViewModel> GetObjList(ContactViewModel item, int startRowIndex, int maxRow)
        {
            return GetObjList(item.Id, item.CompanyId, item.ProjectId, item.ProjectName, item.ContactID,item.ContactName, item.LoginUserID, startRowIndex, maxRow);
        }

        public override ContactViewModel GetObjList(ContactViewModel item)
        {
            return GetObjList(item.Id, item.CompanyId, item.ProjectId, item.ProjectName, item.ContactID, item.ContactName, item.LoginUserID, 1, 1).FirstOrDefault();
        }
    }
}

