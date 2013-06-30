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



        
        public string Up()
        {
            sb.Clear();
            sb.BeginTransaction();
            _Up();
            sb.CommitTransaction();
            return sb.GetScript();
        }

        public string Down()
        {
            sb.Clear();
            sb.BeginTransaction();
            _Down();
            sb.CommitTransaction();
            return sb.GetScript();
        }



        public string Up(string scriptFilePath)
        {
            string script = Up();


            File.WriteAllText(scriptFilePath.Replace(".sql",".change.sql"), script);

            return script;
        }

        public string Down(string scriptFilePath)
        {
            string script = Down();

            File.WriteAllText(scriptFilePath.Replace(".sql", ".rollback.sql"), script);

            return script;
        }


        public string Up(DatabaseConnection conn)
        {
            string script = Up();

            SqlScriptExecuter.ExecuteMigrtionScript(script, conn.ConnectionString,conn.ProviderName,conn.ServerType);

            return script;
        }

        public string Down(DatabaseConnection conn)
        {
            string script = Down();

            SqlScriptExecuter.ExecuteMigrtionScript(script, conn.ConnectionString, conn.ProviderName, conn.ServerType);
          
            return script;
        }

     
    }
}
