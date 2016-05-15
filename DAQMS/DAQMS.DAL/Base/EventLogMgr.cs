using System; 
using System.Diagnostics;

namespace DAL.Base
{
    ///<summary> 
    /// Calls used to write to the event log 
    ///</summary> 

    public class EventLogMgr
    {

        public const string LogName = "AproWebApplication";
        // will be parameterized 

        public EventLogMgr()
        {

        }

        public void Write(EventLogEntryType logType, string strAppName, string strClass, string strMessage)
        {

            EventLog el = default(EventLog);
            string strEventText = null;

            if (EventLog.LogNameFromSourceName(strAppName, ".") != EventLogMgr.LogName)
            {

                if (EventLog.SourceExists(strAppName))
                {
                    EventLog.DeleteEventSource(strAppName);
                }
            }


            //associate with correct folder. 
            if (!EventLog.SourceExists(strAppName))
            {
                EventLog.CreateEventSource(strAppName, EventLogMgr.LogName);
            }

            strEventText = "Class: " + strClass + "\\nMessage: " + strMessage;
            el = new EventLog(EventLogMgr.LogName, ".", strAppName);
            el.WriteEntry(strEventText, logType);
            el.Close();
            el = null;

        }
    }
}




