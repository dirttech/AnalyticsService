
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Configuration.Provider;

namespace AnalyticsService
{
    public class LoggingEvent
    {
        public LoggingEvent()
        {
            
        }      

        private string eventID;
        public string EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        private string userID;
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

    }
    /// <summary>
    /// Summary description for LoggerService
    /// </summary>
    [WebService(Namespace = "http://mylogger.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
   // [System.Web.Script.Services.ScriptService]
    public class LoggerService : System.Web.Services.WebService
    {
        #region Feilds

        private static string connString = ConfigurationManager.ConnectionStrings["AnalyticServiceConnectionString"].ConnectionString;

        private static DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["AnalyticServiceConnectionString"].ProviderName);
        private static string parmPrefix = "@";

        #endregion

        [WebMethod]
        public bool TestLogEvent()
        {
            LoggingEvent logObj = new LoggingEvent();
            logObj.UserID = "test";
            logObj.EventID = "tetevent";

            bool abc = LogEvent(logObj);
            return abc;
               
        }

        #region Methods
        [WebMethod]
        public bool LogEvent(LoggingEvent logObj)
        {
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery;
                        sqlQuery = "Insert into event_log (ID, event_id, user_id, dated) VALUES " +
                               "(@ID, @eventID, @userID, @dated)";                             

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "ID";
                        sNewId.Value = Guid.NewGuid();
                        cmd.Parameters.Add(sNewId);

                        DbParameter seventID = provider.CreateParameter();
                        seventID.ParameterName = parmPrefix + "eventID";
                        seventID.Value = logObj.EventID;
                        cmd.Parameters.Add(seventID);

                        DbParameter suserID = provider.CreateParameter();
                        suserID.ParameterName = parmPrefix + "userID";
                        suserID.Value = logObj.UserID;
                        cmd.Parameters.Add(suserID);

                        DbParameter sDated = provider.CreateParameter();
                        sDated.ParameterName = parmPrefix + "dated";
                        sDated.Value = DateTime.Now;
                        cmd.Parameters.Add(sDated);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool LogEventHostel(LoggingEvent logObj)
        {
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery;
                        sqlQuery = "Insert into event_log_hostel (ID, event_id, user_id, dated) VALUES " +
                               "(@ID, @eventID, @userID, @dated)";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "ID";
                        sNewId.Value = Guid.NewGuid();
                        cmd.Parameters.Add(sNewId);

                        DbParameter seventID = provider.CreateParameter();
                        seventID.ParameterName = parmPrefix + "eventID";
                        seventID.Value = logObj.EventID;
                        cmd.Parameters.Add(seventID);

                        DbParameter suserID = provider.CreateParameter();
                        suserID.ParameterName = parmPrefix + "userID";
                        suserID.Value = logObj.UserID;
                        cmd.Parameters.Add(suserID);

                        DbParameter sDated = provider.CreateParameter();
                        sDated.ParameterName = parmPrefix + "dated";
                        sDated.Value = DateTime.Now;
                        cmd.Parameters.Add(sDated);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
      

        #endregion

    }
}
