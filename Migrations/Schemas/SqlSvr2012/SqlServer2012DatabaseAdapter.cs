using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Migrations.Schemas
{
    public class SqlServer2012DatabaseAdapter : IDatabaseAdapter
    {

        public int ExecuteNonQuery(string query, string connectionString, string providerName)
        {
            int effRows = -1;

            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (DbCommand command = factory.CreateCommand())
                {
                    command.CommandTimeout = 60;

                    command.Connection = connection;

                    command.CommandType = CommandType.Text;

                    command.CommandText = query;

                    connection.Open();

                    effRows = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return effRows;
        }


        public string ExecuteScalar(string query, string connectionString, string providerName, string nullDefault)
        {

            object result = null;

            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

            using (DbConnection connection = factory.CreateConnection())
            {

                connection.ConnectionString = connectionString;

                using (DbCommand command = factory.CreateCommand())
                {

                    command.CommandTimeout = 60;

                    command.Connection = connection;

                    command.CommandType = CommandType.Text;

                    command.CommandText = query;

                    connection.Open();

                    result = command.ExecuteScalar();

                    connection.Close();
                }
            }

            if (result == null || result == DBNull.Value) return nullDefault;

            return result.ToString();
        }

        public string ExecuteQuery(string query, string connectionString, string providerName)
        {
            StringBuilder sb = new StringBuilder();

            object result = null;

            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

            using (DbConnection connection = factory.CreateConnection())
            {

                connection.ConnectionString = connectionString;

                using (DbCommand command = factory.CreateCommand())
                {

                    command.CommandTimeout = 60;

                    command.Connection = connection;

                    command.CommandType = CommandType.Text;

                    command.CommandText = query;

                    connection.Open();

                    DbDataReader db = command.ExecuteReader();

                    if (db.HasRows)
                    {
                        while (db.Read())
                        {
                            result = db["VersionTimestamp"];

                            sb.AppendLine(result.ToString());
                        }
                    }


                    connection.Close();
                }
            }

            string resultstr = sb.ToString();

            return resultstr;
        }
    }

}
