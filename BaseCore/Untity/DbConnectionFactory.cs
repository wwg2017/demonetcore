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
            //这样根据类型确定连接是mysql还是sqlserver
            var type = Type.GetType(driverType);
            var iconn = Activator.CreateInstance(type, connectionStr) as IDbConnection;
            if (iconn == null) { throw new Exception(" is null"); }
            return iconn;
        }
    }
}
