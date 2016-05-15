using System;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.Common;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using DAL.Base;
using Npgsql;
using NpgsqlTypes;

namespace DAL.Models
{
    public class DBHelper : IPrimaryBase
    {
        #region Private Member
        private String strErrorMessage;
        private Int32 intIdentityValue = -1;
        private Int32 intErrorCode = 0;
        #endregion

        #region Public Property
        public Int32 IdentityValue
        {
            set { this.intIdentityValue = value; }
            get { return this.intIdentityValue; }
        }

        public String ReturnMessage
        {
            set { this.strErrorMessage = value; }
            get { return this.strErrorMessage; }
        }

        public Int32 ErrorCode
        {
            set { this.intErrorCode = value; }
            get { return intErrorCode; }
        }
        public bool IsValidExecution { set; get; }

        #endregion

        #region Execute Non Query
        /// <summary>
        /// Return the number the rows affected in the database 
        /// </summary>
        /// <param name="parameterValues">Parameter Values Array of Type Object</param>
        /// <param name="outValueofParam">Not Required can be passed dummy object </param>
        /// <param name="objIdbTransaction">if you want to maintain the transaction</param>
        /// <param name="spno">storedprocedure number you want to execute</param>
        /// <returns>int </returns>
        /// <example>Rows affected </example>

        public int ExecuteNonQuery(CustomParameterList paramList, string CommandName)
        {
            try
            {
                //Get Command Instacne
                IDbCommand objIdbCommand = DataProvider.GetCommandInstance();

                using (IDbConnection objIdbConnection = DataProvider.GetConnectionInstance())
                {
                    //Prepare command
                    PrepareCommand(objIdbCommand, objIdbConnection, null, paramList, CommandName);
                    objIdbCommand.CommandType = CommandType.StoredProcedure;

                    //Open connection
                    if (objIdbCommand.Connection.State == ConnectionState.Closed)
                    {
                        objIdbCommand.Connection.Open();

                    }

                    //Execute command
                 this.IdentityValue=   objIdbCommand.ExecuteNonQuery();
                    //Close connection
                    objIdbCommand.Connection.Close();
                }
                //
                //Validate Execution to identify any error exist
               // ValidateExecution(objIdbCommand);
                //
                if (this.IdentityValue<0)
                {
                    throw new CaughtException(this.ErrorCode, this.ErrorCode.ToString() + ":" + this.ReturnMessage, this, "ExecuteNonQuery");
                }


            }
            catch (Exception ex)
            {
                string str = "Exception(Helper) : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException(Helper) : " + ex.Message;
                }
                throw new Exception(str, ex);
            }

            return this.IdentityValue;
        }

        public int ExecuteNonQuerySQLException(CustomParameterList paramList, string CommandName)
        {
            try
            {
                //Get Command Instacne
                IDbCommand objIdbCommand = DataProvider.GetCommandInstance();

                using (IDbConnection objIdbConnection = DataProvider.GetConnectionInstance())
                {
                    //Prepare command
                    PrepareCommand(objIdbCommand, objIdbConnection, null, paramList, CommandName);
                    objIdbCommand.CommandType = CommandType.StoredProcedure;

                    //Open connection
                    if (objIdbCommand.Connection.State == ConnectionState.Closed)
                    {
                        objIdbCommand.Connection.Open();

                    }
                   // objIdbCommand.CommandType = CommandType.StoredProcedure;

                    //Execute command
                    this.IdentityValue=objIdbCommand.ExecuteNonQuery();
                    //Close connection
                    objIdbCommand.Connection.Close();
                }
                //
                //Validate Execution to identify any error exist
                //ValidateExecution(objIdbCommand);
                //
                if (this.IdentityValue==-1)
                {
                    throw new CaughtException(this.ErrorCode, this.ErrorCode.ToString() + ":" + this.ReturnMessage, this, "ExecuteNonQuery");
                }


            }
            catch (Exception ex)
            {
                string str = "Exception(Helper) : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException(Helper) : " + ex.Message;
                }
                throw new Exception(str, ex);
            }

            return this.IdentityValue;
        }


        /// <summary>
        /// Execute non query with Transaction
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="objIdbTransaction"></param>
        /// <param name="objConnection"></param>
        /// <param name="CommandName"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CustomParameterList paramList, IDbTransaction objIdbTransaction, IDbConnection objConnection, string CommandName)
        {
            try
            {
                //Get command Instance
                IDbCommand objIdbCommand = DataProvider.GetCommandInstance();

                //Prepare command
                PrepareCommand(objIdbCommand, objConnection, objIdbTransaction, paramList, CommandName);

                //if connection is close or not open
                if (objIdbCommand.Connection.State == ConnectionState.Closed)
                {
                    objIdbCommand.Connection.Open();

                }

                objIdbCommand.CommandType = CommandType.StoredProcedure;
                //Execute command
               this.IdentityValue = objIdbCommand.ExecuteNonQuery();

                //Validate Execution to identify any error exist
               // ValidateExecution(objIdbCommand);
                // if error return -1
               if (this.IdentityValue<0)
                {
                    throw new CaughtException(this.ErrorCode, this.ErrorCode.ToString() + ":" + this.ReturnMessage, this, "ExecuteNonQuery");
                }

            }
            catch (Exception ex)
            {
                string str = "Exception(Helper) : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException(Helper) : " + ex.Message;
                }
                throw new Exception(str, ex);
            }

            //return Identity value/id/primarykey
            return this.IdentityValue;


        }

        #endregion

        #region Execute Query
        /// <summary>
        /// Execute query and return a data table
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="objIdbTransaction"></param>
        /// <param name="CommandName"></param>
        /// <returns></returns>
        public DataTable ExecuteQueryToDataTable(CustomParameterList paramList, string CommandName)
        {
            DataSet ds = new DataSet(CommandName);
            DataTable dt = new DataTable();

            try
            {
              //  IDbCommand objIdbCommand = DataProvider.GetCommandInstance();
               
                NpgsqlCommand objIdbCommand = new NpgsqlCommand();

                using (IDbConnection objIdbConnection = DataProvider.GetConnectionInstance())
                {
                    //
                    PrepareCommand(objIdbCommand, objIdbConnection, null, paramList, CommandName);
                   
                    //Open connection
                    if (objIdbCommand.Connection.State == ConnectionState.Closed)
                    {
                        objIdbCommand.Connection.Open();

                    }
                    //
                    objIdbCommand.ExecuteNonQuery();
                    objIdbCommand.CommandText = "fetch all in \"ref\"";
                    objIdbCommand.CommandType = CommandType.Text;

                    using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(objIdbCommand))
                    {
                        Adpt.Fill(dt);
                    }

                   // IDataAdapter objIdbAdapter = DataProvider.GetAdapterInstance(objIdbCommand);
                   // objIdbAdapter.Fill(ds);
                    objIdbCommand.Connection.Close();

                   // dt = ds.Tables[0];

                    ////

                    //objIdbCommand.ExecuteNonQuery();
                    //objIdbCommand.CommandText = "fetch all in \"<unnamed portal 1>\"";
                    //objIdbCommand.CommandType = CommandType.Text;

                    //NpgsqlDataReader dr = objIdbCommand.ExecuteReader();                   
                    //dt.Load(dr);
                    //objIdbCommand.Connection.Close();

                   foreach( DataRow dr in dt.Rows)
                    {
                        Console.Write("{0}\t{1} \n", dr[0], dr[1]);
                    }
                    Console.ReadKey();

                }

                //Validate Execution to identify any error exist
               // ValidateExecution(objIdbCommand);

                if (dt.Rows.Count > 1)
                {
                    if (dt.Rows[1].Field<int>(0) == -1)
                    {
                        throw new CaughtException(this.ErrorCode, this.ErrorCode.ToString() + ":" + this.ReturnMessage, this, "ExecuteNonQuery");
                    }
                }
                
            }

            catch (Exception ex)
            {
                string str = "Exception(Helper) : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException(Helper) : " + ex.Message;
                }
                throw new Exception(str, ex);
            }
            return dt;


        }

        public DataTable ExecuteQueryToDataTable(CustomParameterList paramList, ref object outValueofParam, string CommandName)
        {

            DataSet ds = new DataSet(CommandName);

            try
            {

                IDbCommand objIdbCommand = DataProvider.GetCommandInstance();

                using (IDbConnection objIdbConnection = DataProvider.GetConnectionInstance())
                {

                    PrepareCommand(objIdbCommand, objIdbConnection, null, paramList, CommandName);

                    //Open connection
                    if (objIdbCommand.Connection.State == ConnectionState.Closed)
                    {
                        objIdbCommand.Connection.Open();

                    }

                    IDataAdapter objIdbAdapter = DataProvider.GetAdapterInstance(objIdbCommand);
                    objIdbAdapter.Fill(ds);

                    if (objIdbCommand.Parameters.Contains("@numTotalRows"))
                    {

                        if (AppSettings.DBProvider == "sqlserver")
                        {

                            SqlParameter p;
                            p = (SqlParameter)objIdbCommand.Parameters["@numTotalRows"];
                            outValueofParam = p.Value;
                        }

                    }



                    objIdbCommand.Connection.Close();

                }

                //Validate Execution to identify any error exist
              //  ValidateExecution(objIdbCommand);
               
                // check error-- return -1
                if (ds.Tables[0].Rows[0].Field<int>(0) < 0)
                {
                    throw new CaughtException(this.ErrorCode, this.ErrorCode.ToString() + ":" + this.ReturnMessage, this, "ExecuteNonQuery");
                }

            }

            catch (Exception ex)
            {
                string str = "Exception(Helper) : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException(Helper) : " + ex.Message;
                }
                throw new Exception(str, ex);
            }

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// Return the DataSet object .
        /// It gets the data in dataset from
        /// Database.
        /// </summary>
        /// <param name="parameterValues">Parameter Values Array of Type Object</param>
        /// <param name="outValueofParam">Not Required can be passed dummy object </param>
        /// <param name="objIdbTransaction">if you want to maintain the transaction</param>
        /// <param name="spno">storedprocedure number you want to execute</param>
        /// <returns>dataset</returns>
        public DataSet ExecuteDataSet(CustomParameterList paramList, ref object outValueofParam, IDbTransaction objIdbTransaction, string CommandName)
        {
            IDbCommand objIdbCommand = DataProvider.GetCommandInstance();
            DataSet dsValues = null;

            using (IDbConnection objIdbConnection = DataProvider.GetConnectionInstance())
            {
                PrepareCommand(objIdbCommand, objIdbConnection, objIdbTransaction, paramList, CommandName);
                IDataAdapter objIdbAdapter = DataProvider.GetAdapterInstance(objIdbCommand);
                dsValues = new DataSet();
                objIdbAdapter.Fill(dsValues);
            }
            return dsValues;

        }

        #endregion

        #region Prepare Command
        /// <summary>
        /// Prepare the command and 
        /// set it's attributes
        /// </summary>
        /// <param name="objIdbCommand"></param>
        /// <param name="objIdbConnection"></param>
        /// <param name="objIdbTransaction"></param>
        /// <param name="parameterValues"></param>
        /// <param name="spno"></param>
        private static void PrepareCommand(IDbCommand objIdbCommand, IDbConnection objIdbConnection, IDbTransaction objIdbTransaction, CustomParameterList paramList, string CommandName)
        {
            objIdbCommand.Connection = objIdbConnection;
            try
            {
                objIdbCommand.CommandType = CommandType.StoredProcedure;
                if (objIdbTransaction != null)
                {
                    objIdbCommand.Transaction = objIdbTransaction;
                }
                CommandParamterMaker(objIdbCommand, paramList, CommandName);
            }
            catch (Exception ex)
            {
                string str = "Exception(Helper) : " + ex.Message;
                if (ex.InnerException != null)
                {
                    str += "InnerException(Helper) : " + ex.Message;
                }
                throw new Exception(str, ex);
            }

        }

        /// <summary>
        /// Reterive the parameter the from the xml files
        /// and set into the parameters.
        /// </summary>
        /// <param name="objIdbCommand"></param>
        /// <param name="parameterValues"></param>
        /// <param name="spno"></param>
        /// <returns></returns>
        private static IDataParameter[] CommandParamterMaker(IDbCommand objIdbCommand, CustomParameterList paramList, string ProcedureName)
        {
            //set procedure name
            objIdbCommand.CommandText = ProcedureName;
            //Get param array instance for supplied number of parameter
            //add 2 more field for two output fixed parameter;
            IDataParameter[] IDBParameter = DataProvider.GetParametersInstances(paramList.Count);

            

            int paramcounter = 0;

            foreach (CustomParameter param in paramList)
            {
                //Get parameter instance
                IDBParameter[paramcounter] = DataProvider.GetParameterInstance();
                //Set parameter name
                IDBParameter[paramcounter].ParameterName = param.ParamaterName;
                //Set Parameter direction
                IDBParameter[paramcounter].Direction = param.ParamDirection;

                //Manage parameter value
                if (param.ParamValue == null)
                {
                    if (param.ParamType == NpgsqlDbType.Varchar)
                    {
                        IDBParameter[paramcounter].Value = "";
                    }
                    else
                    {
                        IDBParameter[paramcounter].Value = DBNull.Value;
                    }

                }
                else if (param.ParamType == NpgsqlDbType.Timestamp)
                {
                    if (Convert.ToDateTime(param.ParamValue) == DateTime.MinValue || Convert.ToDateTime(param.ParamValue) == DateTime.MaxValue)
                    {
                        IDBParameter[paramcounter].Value = DBNull.Value;
                    }
                    else
                    {
                        IDBParameter[paramcounter].Value = param.ParamValue;
                    }
                }
               
         
                else
                {
                    IDBParameter[paramcounter].Value = param.ParamValue;
                }
                //add parameter to paramater list                                                           
                objIdbCommand.Parameters.Add(IDBParameter[paramcounter]);
                //update counter
                paramcounter++;
            }

/*
            //Manage output parameter
            IDBParameter[paramcounter] = DataProvider.GetParameterInstanceForOutput(AppSettings.OutputPramPrefix + "Errorcode");
            IDBParameter[paramcounter].DbType = System.Data.DbType.Int32;
            IDBParameter[paramcounter].Direction = ParameterDirection.Output;
            objIdbCommand.Parameters.Add(IDBParameter[paramcounter]);
            //
            paramcounter++;
            IDBParameter[paramcounter] = DataProvider.GetParameterInstanceForOutput(AppSettings.OutputPramPrefix + "Errormessage");
            IDBParameter[paramcounter].DbType = System.Data.DbType.String;
            ((IDbDataParameter)IDBParameter[paramcounter]).Size = 200;
            IDBParameter[paramcounter].Direction = ParameterDirection.Output;
            objIdbCommand.Parameters.Add(IDBParameter[paramcounter]);
            //
            paramcounter++;
            IDBParameter[paramcounter] = DataProvider.GetParameterInstanceForOutput(AppSettings.OutputPramPrefix + "IdentityValue");
            IDBParameter[paramcounter].DbType = System.Data.DbType.Int32;
            IDBParameter[paramcounter].Direction = ParameterDirection.Output;
            objIdbCommand.Parameters.Add(IDBParameter[paramcounter]);
*/

            //return parameter list
            return IDBParameter;

        }

        #endregion

        #region Validate Execution
        /// <summary>
        /// Validate Execution
        /// </summary>
        /// <param name="objIdbCommand"></param>
        private void ValidateExecution(IDbCommand objIdbCommand)
        {
            //read output parameter value
            this.IdentityValue = Convert.ToInt32(((IDbDataParameter)objIdbCommand.Parameters[AppSettings.OutputPramPrefix + "IdentityValue"]).Value);
            this.ReturnMessage = Convert.ToString(((IDbDataParameter)objIdbCommand.Parameters[AppSettings.OutputPramPrefix + "Errormessage"]).Value);
            this.ErrorCode = Convert.ToInt32(((IDbDataParameter)objIdbCommand.Parameters[AppSettings.OutputPramPrefix + "Errorcode"]).Value);
            this.IsValidExecution = (this.ErrorCode == 0);
        }
        #endregion

        #region Object Creator
        /// <summary>
        /// Get Transaction Instance
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IDbTransaction GetTransaction(IDbConnection connection)
        {
            return DataProvider.GetTransactionInstacne(connection);
        }
        /// <summary>
        /// 
        //Get connection Instance
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection()
        {
            return DataProvider.GetConnectionInstance();
        }


        public static DBHelper GetInstance()
        {
            return new DBHelper();
        }

        #endregion

        #region IPrimaryBase Members
        public string AppName { set; get; }
        #endregion


    }


}