using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations
{
    public class DatabaseConnection
    {
      
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public string ServerType { get; set; }

        public DatabaseConnection(string connectionString, string providerName, string serverType)
        {
            ConnectionString = connectionString;

            ProviderName = providerName;

            ServerType = serverType;

        }

    }
}
