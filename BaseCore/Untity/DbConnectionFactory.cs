using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BaseCore.Untity
{
    public class DbConnectionFactory
    {
        public static IDbConnection GetInstance(String driverType, string connectionStr)
        {
            var type = Type.GetType(driverType);
            var iconn = Activator.CreateInstance(type, connectionStr) as IDbConnection;
            if (iconn == null) { throw new Exception(" is null"); }
            return iconn;
        }
    }
}
