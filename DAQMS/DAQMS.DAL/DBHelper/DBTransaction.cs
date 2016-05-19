using System;
using System.Data.SqlClient;
using DAQMS.DAL.Models;

namespace DAQMS.DAL
{
    public class DBTransaction : IDisposable
    {
        // for Oracle
        //internal OracleTransaction CurrentTransaction = null;
        //private OracleConnection conn = new OracleConnection(Connection.ConnectionString);
        //-----
        //--- for SQL Server
        internal SqlTransaction CurrentTransaction = null;
        
        private SqlConnection _conn = null;

        public SqlConnection Connection
        {
            get { return _conn; }
        }
        
        public void Begin()
        {
            _conn = DataProvider.GetConnectionInstance() as SqlConnection;// only for sql server
            _conn.Open();
            CurrentTransaction = _conn.BeginTransaction();
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
            _conn.Dispose();

        }
        
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}
