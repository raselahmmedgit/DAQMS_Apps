using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using Npgsql;
using NpgsqlTypes;

namespace DAQMS.DAL.Models
{
    public class DataProvider
    {
        #region Constants And Class Varibles
        private const string SQLSERVER = "sqlserver";
        private const string ORACLE = "Oracle";
        private const string ODBC = "Odbc";
        private const string MYSQL = "MySql";
        private const string OTHER = "Other";
        private const string PG = "PG";
        #endregion
        //NpgsqlConnection 
        #region Get Connection Instacne
        public static IDbConnection GetConnectionInstance()
        {
            IDbConnection objIdbConnection = null;
            switch (AppSettings.DBProvider)
            {
                case SQLSERVER:
                    {
                        objIdbConnection = new SqlConnection(AppSettings.DBConnectionString);
                        break;
                    }

                case PG:
                    {
                        objIdbConnection = new NpgsqlConnection(AppSettings.DBConnectionString);
                        break;
                    }
                //case ORACLE:
                //    {
                //        objIdbConnection = new OracleConnection(AppSettings.DBConnectionString);
                //        break;
                //    }
                case ODBC:
                    {
                        objIdbConnection = new OdbcConnection(AppSettings.DBConnectionString);
                        break;
                    }
                //case MYSQL:
                //    {
                //        objIdbConnection = new MySqlConnection(AppSettings.DBConnectionString);                        
                //        break;
                //    }
                case OTHER:
                    {
                        objIdbConnection = new OleDbConnection(AppSettings.DBConnectionString);
                        break;
                    }
            }
            return objIdbConnection;
        }
        #endregion

        #region Get Command Instance
        public static IDbCommand GetCommandInstance()
        {
            IDbCommand objIdbComand = null;

            switch (AppSettings.DBProvider)
            {
                case SQLSERVER:
                    {
                        objIdbComand = new SqlCommand();
                        break;
                    }
                case ORACLE:
                    {
                        objIdbComand = new OleDbCommand();
                        break;
                    }
                case ODBC:
                    {
                        objIdbComand = new OdbcCommand();
                        break;
                    }
                case PG:
                    {
                        objIdbComand = new NpgsqlCommand();
                        break;
                    }
                    
                //case MYSQL:
                //    {
                //        objIdbComand = new MySqlCommand();
                //        break;
                //    }

                case OTHER:
                    {
                        objIdbComand = new OleDbCommand();
                        break;
                    }
            }

            return objIdbComand;
        }

        #endregion

        #region Get Parameter Instacne

        public static IDataParameter[] GetParametersInstances(int parmaterCount)
        {
            IDataParameter[] idbParamters = null;
            if (parmaterCount > 0)
            {

                switch (AppSettings.DBProvider)
                {
                    case SQLSERVER:
                        {
                            idbParamters = new SqlParameter[parmaterCount];
                            break;
                        }
                    case PG:
                        {
                            idbParamters = new  NpgsqlParameter[parmaterCount];
                            break;
                        }
                    case ORACLE:
                        {
                            idbParamters = new OleDbParameter[parmaterCount];
                            break;
                        }
                    case ODBC:
                        {
                            idbParamters = new OdbcParameter[parmaterCount];
                            break;
                        }
                    //case MYSQL:
                    //    {
                    //        idbParamters = new MySqlParameter[parmaterCount];
                    //        break;
                    //    }
                    case OTHER:
                        {
                            idbParamters = new OleDbParameter[parmaterCount];
                            break;
                        }
                }
            }
            return idbParamters;
        }

        public static IDataParameter GetParameterInstance()
        {
            IDataParameter idbParamters = null;

            switch (AppSettings.DBProvider)
            {
                case SQLSERVER:
                    {
                        idbParamters = new SqlParameter();                        
                        break;
                    }
                case PG:
                    {
                        idbParamters = new NpgsqlParameter();
                        break;
                    }
                case ORACLE:
                    {
                        idbParamters = new OleDbParameter();
                        break;
                    }

                case ODBC:
                    {
                        idbParamters = new OdbcParameter();
                        break;
                    }
                //case MYSQL:
                //    {
                //        idbParamters = new MySqlParameter();
                //        break;
                //    }

                case OTHER:
                    {
                        idbParamters = new OleDbParameter();
                        break;
                    }
            }
            return idbParamters;
        }

        public static IDataParameter GetParameterInstanceForOutput(string strParamName)
        {
            IDataParameter idbParamters = null;

            switch (AppSettings.DBProvider)
            {
                case SQLSERVER:
                    {
                        idbParamters = new SqlParameter(strParamName, SqlDbType.VarChar, 200);
                        break;
                    }
                case PG:
                    {
                        idbParamters = new NpgsqlParameter(strParamName, NpgsqlDbType.Varchar, 200);
                        break;
                    }
                //case ORACLE:
                //    {
                //        idbParamters = new OracleParameter( strParamName,OracleType.VarChar, 200);
                //        break;
                //    }
                case ODBC:
                    {
                        idbParamters = new OdbcParameter(strParamName, OdbcType.VarChar, 200);
                        break;
                    }
                //case MYSQL:
                //    {
                //        idbParamters = new MySqlParameter(strParamName, MySqlDbType.VarChar, 200);
                //        break;
                //    }
                case OTHER:
                    {
                        idbParamters = new OdbcParameter(strParamName,OdbcType.VarChar,  200);
                        break;
                    }
            }
            return idbParamters;
        }

        #endregion

        #region GetAdapter Instacne

        public static IDataAdapter GetAdapterInstance(IDbCommand objIdbCommand)
        {
            IDataAdapter objIdbAdapter = null;

            switch (AppSettings.DBProvider)
            {
                case SQLSERVER:
                    {
                        objIdbAdapter = new SqlDataAdapter((SqlCommand)objIdbCommand);
                        break;
                    }
                case PG:
                    {
                        objIdbAdapter = new NpgsqlDataAdapter((NpgsqlCommand)objIdbCommand);
                        break;
                    }
                case ORACLE:
                    {
                        objIdbAdapter = new OleDbDataAdapter((OleDbCommand)objIdbCommand);
                        break;
                    }
                case ODBC:
                    {
                        objIdbAdapter = new OdbcDataAdapter((OdbcCommand)objIdbCommand);
                        break;
                    }
                //case MYSQL:
                //    {
                //        objIdbAdapter = new MySqlDataAdapter((MySqlCommand)objIdbCommand);
                //        break;
                //    }

                case OTHER:
                    {
                        objIdbAdapter = new OleDbDataAdapter((OleDbCommand)objIdbCommand);
                        break;
                    }
            }

            return objIdbAdapter;
        }

        #endregion

        #region Get Transaction Instacne

        public static IDbTransaction GetTransactionInstacne(IDbConnection connection)
        {
            IDbTransaction  objIdbTransaction = null;

            switch (AppSettings.DBProvider)
            {
                case SQLSERVER:
                    {
                        objIdbTransaction = (SqlTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        break;
                    }
                case PG:
                    {
                        objIdbTransaction = (NpgsqlTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        break;
                    }
                //case ORACLE:
                //    {
                //        objIdbTransaction = (OracleTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);
                //        break;
                //    }
                case ODBC:
                    {
                        objIdbTransaction = (OdbcTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        break;
                    }
                //case MYSQL:
                //    {
                //        objIdbTransaction = (MySqlTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);
                //        break;
                //    }

                case OTHER:
                    {

                        objIdbTransaction = (OleDbTransaction)connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        break;
                    }
            }

            return objIdbTransaction;

        }

        #endregion

    }
}
