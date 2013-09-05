using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Migrations
{
    public abstract class LockedMigration
    {
        public ISchemaBuilder sb;
        public IColumnType c;
        public INullabilityType n;
        public IDefaultValue d;

        protected abstract void _Up();

        protected abstract void _Down();


        public string Up()
        {
            throw new Exception("Locked Migration");

        }

        public string Down()
        {
            throw new Exception("Locked Migration");

        }


        public string Up(string scriptFilePath)
        {
            throw new Exception("Locked Migration");
        }

        public string Down(string scriptFilePath)
        {

            throw new Exception("Locked Migration");
        }


        public string Up(DatabaseConnection conn)
        {


            throw new Exception("Locked Migration");
        }

        public string Down(DatabaseConnection conn)
        {


            throw new Exception("Locked Migration");
        }


    }

    public abstract class Migration
    {
        public ISchemaBuilder sb;
        public IColumnType c;
        public INullabilityType n;
        public IDefaultValue d;

        protected abstract void _Up();

        protected abstract void _Down();




        public string Up(string nextVersion)
        {
            sb.Clear();
            sb.BeginTransaction();
            _Up();
            sb.CommitTransaction("update dbo.SchemaVersion set  VersionTimestamp = '" + nextVersion + "'");
            return sb.GetScript();
        }

        public string Down(string prevVersion)
        {
            sb.Clear();
            sb.BeginTransaction();
            _Down();
            sb.CommitTransaction("update dbo.SchemaVersion set  VersionTimestamp = '" + prevVersion + "'");
            return sb.GetScript();
        }



        public string UpScript(string scriptFilePath,string nextVersion)
        {
            string script = Up(nextVersion);


            File.WriteAllText(scriptFilePath.Replace(".sql",".change.sql"), script);

            return script;
        }

        public string DownScript(string scriptFilePath, string prevVersion)
        {
            string script = Down(prevVersion);

            File.WriteAllText(scriptFilePath.Replace(".sql", ".rollback.sql"), script);

            return script;
        }


        public string Up(DatabaseConnection conn,string nextVersion)
        {
            string script = Up(nextVersion);

            SqlScriptExecuter.ExecuteMigrtionScript(script, conn.ConnectionString,conn.ProviderName,conn.ServerType);

            SqlScriptExecuter.UpdateSchemaVersion(nextVersion, conn.ConnectionString, conn.ProviderName, conn.ServerType);


            return script;
        }

        public string Down(DatabaseConnection conn, string prevVersion)
        {
            string script = Down(prevVersion);

            SqlScriptExecuter.ExecuteMigrtionScript(script, conn.ConnectionString, conn.ProviderName, conn.ServerType);

           
            SqlScriptExecuter.UpdateSchemaVersion(prevVersion, conn.ConnectionString, conn.ProviderName, conn.ServerType);
           
            return script;
        }

     
    }
}
