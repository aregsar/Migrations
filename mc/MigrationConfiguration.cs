using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace mc
{
    public class MigrationConfiguration
    {
        public static string ServerType { get; set; }
        public static string ProviderName { get; set; }
        public static string ConnectionString { get; set; }
        public static string MasterConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static string migrationClassPath { get; set; }
        public static string migrationScriptPath { get; set; }
        public static string migrationLibraryAssemblyPath { get; set; }

       
        static MigrationConfiguration()
        {
             

            ServerType = ConfigurationManager.AppSettings["serverType"].ToString();
            ProviderName = ConfigurationManager.AppSettings["providerName"].ToString();
            MasterConnectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
            DatabaseName = ConfigurationManager.AppSettings["databaseName"].ToString();
            migrationClassPath = ConfigurationManager.AppSettings["migrationClassPath"].ToString();
            migrationScriptPath = ConfigurationManager.AppSettings["migrationScriptPath"].ToString();
            migrationLibraryAssemblyPath = ConfigurationManager.AppSettings["migrationLibraryAssemblyPath"].ToString();
            ConnectionString = MasterConnectionString.Replace("Master",DatabaseName);
        }

    }
}
