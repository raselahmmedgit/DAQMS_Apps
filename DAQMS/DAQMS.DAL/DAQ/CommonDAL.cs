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


namespace DAQMS.DAL.DAQ
{
   public class CommonDAL
    {
       public static CommonDAL GetInstance()
       {
           return new CommonDAL();
       }

       public List<NameValue> GetObjList(string classType, int id, string name, string loginUserID, int startRowIndex, int maximumRows)
       {
           DataSet dsResult = new DataSet();
           DataTable dt = new DataTable();
           NpgsqlConnection conn = null;
          
           string sql = string.Empty;

           List<NameValue> results = null;

           try
           {
               conn = DataProvider.GetConnectionInstance() as NpgsqlConnection;
               conn.Open();

               // Start a transaction as it is required to work with result sets (cursors) in PostgreSQL
               NpgsqlTransaction tran = conn.BeginTransaction();

               //Find SQL
               if (classType == "alert_type")
               {
                   sql = "SELECT * FROM (SELECT id, alert_type as name  FROM alert_type) abc WHERE 0=0";
               }

               if (id>0)
               {
                   sql += " AND id=" + id;
               }

               if (!(System.String.IsNullOrEmpty(name)))
               {
                   sql += " AND name='" + name +"'";
               }

               // Define a command to call procedure
               NpgsqlCommand command = new NpgsqlCommand("sql", conn);
               command.CommandType = CommandType.Text;


               //command.ExecuteNonQuery();
               //command.CommandText = "fetch all in \"ref\"";
               //command.CommandType = CommandType.Text;

               //// Execute the procedure and obtain a result set
               //  NpgsqlDataReader data = command.ExecuteReader();

               using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(command))
               {
                   Adpt.Fill(dsResult);
               }

               // Fetch rows 
               results = new List<NameValue>();
               foreach (DataRow dr in dsResult.Tables[0].Rows)
               {
                   NameValue obj = new NameValue();
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
    }
}
