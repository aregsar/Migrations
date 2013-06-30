using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations
{
    public interface IDatabaseAdapter
    {

        int ExecuteNonQuery(string sqlscript, string connectionString, string providerName);

        string ExecuteScalar(string query, string connectionString, string providerName, string nullDefault);

    }
}
