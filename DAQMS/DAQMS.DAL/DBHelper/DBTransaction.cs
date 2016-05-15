using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL.Models;
using System.Data;

namespace DAL
{
    public class DBTransaction : IDisposable
    {
        // for Oracle
        //internal OracleTransaction CurrentTransaction = null;
        //private OracleConnection conn = new OracleConnection(Connection.ConnectionString);
        //-----
        //--- for SQL Server
        internal SqlTransaction CurrentTransaction = null;
        private SqlConnection conn = null;

        public SqlConnection Connection
        {
            get { return conn; }
        }
        //--------


        public void Begin()
        {
            conn = DataProvider.GetConnectionInstance() as SqlConnection;// only for sql server
            conn.Open();
            CurrentTransaction = conn.BeginTransaction();
        }


        public void Commit()
        {
            CurrentTransaction.Commit();
        }

        public void RollBack()
        {
            CurrentTransaction.Rollback();

        }

        public void Dispose()
        {
            CurrentTransaction.Dispose();
            conn.Dispose();

        }


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}
