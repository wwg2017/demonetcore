using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Model
{
    public class DBConfig
    {
        public QlwDB QlwDB { get; set; }

        public IfsDB IfsDB { get; set; }
    }

    public class QlwDB
    {

        public string DbDriverType { get; set; }
        public string ConnectionString { get; set; }     
    }
    public class IfsDB
    {
        public const string DEFAULT_CONN = "server=10.214.253.22;database=master;uid=info;pwd=QBWV6jg8h2;";
        public string DbDriverType { get; set; }

        public string ConnectionString { get; set; }
    }

}
