using log4net;
using System;

namespace Framework.Helper.Logging
{
    public static class Logging
    {
        //private static readonly ILog Log = LogManager.GetLogger("log4netLogger");
        private static readonly ILog Log = LogManager.GetLogger("ForAllApplication");
        public static void PutError(string message, Exception e)
        {
            Log.Error(message + "; Error: ", e);

            //var logMailFlg = Configuration.Config.GetConfigByKey("SystemMailLog");
            //if (logMailFlg != null)
            //{
            //    var sendMailFlg = Convert.ToBoolean(logMailFlg);
            //    var sysName = Configuration.Config.GetConfigByKey("SystemName") ?? "";
            //    if (sendMailFlg)
            //        LogManager.GetLogger("ECSLogger").Error(sysName + " : " + message, e);
            //}
        }
        public static void PushString(string message)
        {
            Log.Info(message);
        }
        public static void PushStringFormat(string message, object[] value)
        {
            Log.InfoFormat(message, value);
        }
        public static void LogEx(string message, Exception e)
        {
            Log.Error(message + "; Exceptions: ", e);
        }
        // Other Custom Logging Functions

    }
}
