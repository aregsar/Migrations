using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Migrations.Schemas;

namespace Migrations
{
   

    

    public class SqlScriptExecuter
    {
        public static void CreateDatabase(string databaseName, string masterConnectionString, string providerName, string serverType)
        {
            //string sql = "use master create database " + databaseName;

            DatabaseAdapterFactory.GetAdater(serverType).ExecuteNonQuery("create database " + databaseName, masterConnectionString, providerName);        
        }



        public static string CreateSchemaVersionTable(string connectionString, string providerName, string serverType)
        {
            IDatabaseAdapter sa = DatabaseAdapterFactory.GetAdater(serverType);

            string version = "schema version table does not exist";

            try
            {
                version = GetSchemaVersion(connectionString, providerName, serverType);
            }
            catch
            {
                sa.ExecuteNonQuery("create table dbo.SchemaVersion(VersionTimestamp nvarchar(14) not null default('00000000000000'))", connectionString, providerName);

                version = GetSchemaVersion(connectionString, providerName, serverType);
            }


            if (version == "schema version table does not exist")
            {
                sa.ExecuteNonQuery("insert into dbo.SchemaVersion(VersionTimestamp)Values('00000000000000')", connectionString, providerName);
                
                return "created schema version table. current version: 00000000000000";
            }
            else
            {
                return "Schema version already exists. current version: " + version;
            }

        }

        public static string GetSchemaVersion(string connectionString, string providerName, string serverType)
        {
            return DatabaseAdapterFactory.GetAdater(serverType).ExecuteScalar("select VersionTimestamp from dbo.SchemaVersion", connectionString, providerName,"schema version table does not exist");
        }


        public static void UpdateSchemaVersion(string version, string connectionString, string providerName, string serverType)
        {
            DatabaseAdapterFactory.GetAdater(serverType).ExecuteNonQuery("update dbo.SchemaVersion set  VersionTimestamp = '" + version + "'", connectionString, providerName);
        }



        public static void ExecuteMigrtionScript(string migrationScript, string connectionString, string providerName, string serverType)
        {
            DatabaseAdapterFactory.GetAdater(serverType).ExecuteNonQuery(migrationScript, connectionString, providerName);
        }

     
    }
}
