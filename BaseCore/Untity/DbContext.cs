using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BaseCore.Untity
{
    public class DbContext
    {
        public static int Execute(IDbConnection _conn, string sql, object param = null)
        {
            using (IDbConnection conn = _conn)
            {
                return conn.Execute(sql, param, commandTimeout: 600);
            }
        }
    }
}
