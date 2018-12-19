using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Log
{
    public class LoggerManager
    {
        private static ILogger LoggerInstance
        {
            get {
                ILogger logger = LogManager.GetCurrentClassLogger();
                return logger;
            }
        }
        public static void Info(string msg, params object[] args)
        {
            LoggerInstance.Info(msg, args);
        }
        public static void Error(Exception ex,string msg,params object []args)
        {
            LoggerInstance.Error(ex, msg, args);
        }
    }
}
