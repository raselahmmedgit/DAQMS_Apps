using System.Configuration;

namespace DAQMS.DAL.Models
{
    public static class AppSettings
    {
        #region Private Members
        //private static System.String strConnection;
        //private static System.String strProvider;
        //private static System.String strAppName;
        //private static System.String strPrefix;
        #endregion

        #region Public Property
        public static string DBConnectionString
        {
            get
            {
                //return "Server=127.0.0.1;Port=5432;Database=Enovative;User Id=postgres;Password=sa1234;CommandTimeout=20";
                return "Server=localhost;Port=5432;Database=Enovative;User Id=postgres;Password=sa1234;CommandTimeout=20";

               // return ConfigurationManager.AppSettings["AppDbContext"];
                // ConfigurationSettings.AppSettings["AppDbContext"].ToString();
               
            }            
        }

        public static string DBProvider
        {
            get
            {
                return ConfigurationSettings.AppSettings["Provider"].ToString();
            }            
        }

        public static string ApplicationName
        {

            get { return ConfigurationSettings.AppSettings["ApplicationName"].ToString(); }
        }

        public static string OutputPramPrefix
        {            
            get { return ConfigurationSettings.AppSettings["OutputParamPrefix"].ToString(); }
        }


        public static string GetPackageName()
        {
            return "pos.";
            //   return ConfigurationSettings.AppSettings["GetPOSSchema"].ToString();
        }

        public static int ErrorCode = -10000000;
        #endregion
    }
}
