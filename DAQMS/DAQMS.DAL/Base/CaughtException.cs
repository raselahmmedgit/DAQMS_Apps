using System;
using System.Text; 
using System.IO;


namespace DAQMS.DAL.Base
{
    /// <summary> 
    /// class to handle exception 
    /// </summary> 
    /// <remarks></remarks> 
    public class CaughtException :Exception 
    {

        private string _methodName;
        private string _userId;
        private string _errorMessage;
        //private System.Web.UI.Page callingWebPage;
        private string _appName;

        /// <summary> 
        ///Method name where the exception will happend 
        /// </summary> 
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        /// <summary> 
        /// current user Id who is responsible for this exception 
        /// </summary> 
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary> 
        /// Message of the exception 
        /// </summary> 
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public Int32 ErrorCode { set; get; }
        

        /// <summary> 
        /// which web page is responsible for this exception 
        /// </summary> 
        //public System.Web.UI.Page CallingWebPage
        //{
        //    get { return callingWebPage; }
        //    set { callingWebPage = value; }
        //}

        /// <summary> 
        /// Application name where the exception will happend 
        /// </summary> 
        public string AppName
        {
            get { return _appName; }
            set { _appName = value; }
        }

        /// <summary> 
        /// enum to set where the exception message will be store either flat file or log 
        /// </summary> 
        /// <remarks></remarks> 
        public enum WriteLogOption
        {
            LogToEventViewer = 1,
            LogToFileDefaultPath = 2,
            LoagToFileUserDefinePath = 3
        }


        public CaughtException()
        {

        }
        public CaughtException(Exception ex)
        {
            this.ErrorMessage = ex.Message;
        }

        public CaughtException(Exception ex, IPrimaryBase pItem, string strMethodName)
        {
            this.ErrorMessage = ex.Message;
            this.MethodName = strMethodName;
            this.Source = pItem.AppName + " -> " + pItem.GetType().FullName.ToString() + " -> " + MethodName;
            this.AppName = pItem.AppName;
        }

        public CaughtException(string msg, IPrimaryBase pItem, string strMethodName)
        {
            this.ErrorMessage = msg;
            this.MethodName = strMethodName;
            this.Source = pItem.AppName + " -> " + pItem.GetType().FullName.ToString() + " -> " + MethodName;
            this.AppName = pItem.AppName;
        }

        public CaughtException(Int32 errorCode,string msg, IPrimaryBase pItem, string strMethodName)
        {
            this.ErrorMessage = msg;
            this.MethodName = strMethodName;
            this.Source = pItem.AppName + " -> " + pItem.GetType().FullName.ToString() + " -> " + MethodName;
            this.AppName = pItem.AppName;
            this.ErrorCode = errorCode;
        }

        public void SetAttributes(string strMethodName, string strAppName, string strSource)
        {

            this.MethodName = strMethodName;
            this.Source = strSource;
            this.AppName = strAppName;

        }

        public void Log(WriteLogOption lOption,string strPath)
        {

            string strErrorMsg = null;            
            switch (lOption)
            {
                case WriteLogOption.LogToEventViewer:
                    strErrorMsg = this.GetErrorMessage();
                    WriteToEventViewer(strErrorMsg);
                    return;
                case WriteLogOption.LogToFileDefaultPath:
                    strErrorMsg = this.GetErrorMessage();
                    WriteToLog(strErrorMsg);
                    break;
                case WriteLogOption.LoagToFileUserDefinePath:
                    strErrorMsg = this.GetErrorMessage();
                    WriteToLog(strErrorMsg, strPath);
                    return;
                default:
                    return;
            }

        }

        private string GetErrorMessage()
        {

            try
            {

                StringBuilder sbd = new StringBuilder();
                sbd.Append("UserId: " + this.UserId + "\\n");
                sbd.Append("DateTime: " + System.DateTime.Now.ToString() + "\\n");
                sbd.Append("Source: " + this.Source + "\\n");
                sbd.Append("Error Message: " + this.ErrorMessage + "\\n");
                return sbd.ToString();
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        private void WriteToLog(string strMessage, string strPath)
        {
            try
            {
                StreamWriter sw = new StreamWriter(strPath, true, Encoding.ASCII);
                sw.WriteLine(strMessage);
                sw.Close();
            }
            catch// (Exception ex)
            {
            }
        }

        private void WriteToLog(string msg)
        {

            try
            {

                StreamWriter sw = new StreamWriter("c:\\log.txt", true, Encoding.ASCII);
                sw.WriteLine(msg);
                sw.Close();
            }
            catch// (Exception ex)
            {
            }
        }
        
        private void WriteToEventViewer(string strMessage)
        {

            try
            {

                EventLogMgr el = new EventLogMgr();
                el.Write(System.Diagnostics.EventLogEntryType.Error, this.AppName, this.Source, strMessage);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
