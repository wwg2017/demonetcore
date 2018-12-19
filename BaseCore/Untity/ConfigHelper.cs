using BaseCore.Model;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace BaseCore.Untity
{
    public  class ConfigHelper
    {
        private readonly DBConfig _dbConfig;
        public ConfigHelper(IOptionsMonitor<DBConfig> dBConfig)
        {
            _dbConfig = dBConfig.CurrentValue;
        }
        public IDbConnection GetConn(string classValue= "QlwDB")
        {

            IDbConnection conn = null;
            switch (classValue)
            {
                case "QlwDB":
                    conn = DbConnectionFactory.GetInstance(_dbConfig.QlwDB.DbDriverType, _dbConfig.QlwDB.ConnectionString);
                    break;
            }
            return conn;
        }                
    }
}
