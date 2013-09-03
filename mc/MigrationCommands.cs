using System;
using System.Collections;
using System.Linq;
using System.Text;
using Migrations;
using System.IO;
using System.Collections.Generic;

namespace mc
{
    /*
       MigrationFactory  f = new SqlSvr2012MigrationFactory("MigrationFilePath");
             MigrationFactory factory = MigrationFactory.GetFactoryForDatabaseType(MigrationConfiguration.ServerType);  
     * Migration migration = factory.GetMigration(migrationFilePath, MigrationConfiguration.migrationLibraryAssemblyPath);
      //from file version via Factory
      script = f.GetMigration("20120233", MigrationConfiguration.migrationLibraryAssemblyPath).Up();
      script = f.GetMigration("20120233", MigrationConfiguration.migrationLibraryAssemblyPath).Down();

      script = f.GetMigration("20120233", MigrationConfiguration.migrationLibraryAssemblyPath).Up("MigrationFileScriptPath");
      script = f.GetMigration("20120233", MigrationConfiguration.migrationLibraryAssemblyPath).Down("MigrationFileScriptPath");

      script = f.GetMigration("20120233", MigrationConfiguration.migrationLibraryAssemblyPath).Up(conn);
      script = f.GetMigration("20120233", MigrationConfiguration.migrationLibraryAssemblyPath).Down(conn);
      */
    class MigrationCommands
    {
        public string CurrentCommand { get; set; }

        public string ProcessCommand(string commandString)
        {
            CurrentCommand = commandString;

            if (commandString.StartsWith("help")) return ProcessHelpCommand();

            if (commandString.StartsWith("version")) return ProcessVersionCommand(MigrationConfiguration.ConnectionString);

            if (commandString.StartsWith("migrate")) return ProcessMigrateCommand(RemoveFromStartAndTrim("migrate", commandString));

            if (commandString.StartsWith("script")) return ProcessScriptCommand(RemoveFromStartAndTrim("script", commandString));

            if (commandString.StartsWith("print")) return ProcessPrintCommand(RemoveFromStartAndTrim("print", commandString));

            if (commandString.StartsWith("create")) return ProcessCreateCommand(RemoveFromStartAndTrim("create", commandString));

            return "command not available";
        }


        private string ProcessHelpCommand()
        {
            //TOODO: Create a commad that reads all table names and their column names
            //and creates a partial file with table name that has the _tableName property for base entity and static propertie for columnNames
  

            //Ctrl+k, Ctrl+D //reformat
            return "available commands:" + Environment.NewLine + Environment.NewLine
                    + "create database" + Environment.NewLine
                    + "create schema" + Environment.NewLine //return current version 00000000000000
                    + "version    (shortcut: v)" + Environment.NewLine //return current version
                    + "create migration <name>" + Environment.NewLine //return migration file name                
                    + "script <version>" + Environment.NewLine //return down and up scripts
                    + "script all" + Environment.NewLine  //return all down and up scripts
                    + "print <version>" + Environment.NewLine //return down and up scripts
                    + "print all" + Environment.NewLine  //return all down and up scripts
                    + "migrate up" + Environment.NewLine //from current version
                    + "migrate down " + Environment.NewLine //from current version
                    + "migrate to <version>" + Environment.NewLine //from current version
                    + "migrate up all" + Environment.NewLine //from current version
                    + "migrate down all" + Environment.NewLine //from current version
                    ;


        }


        private bool IsVerionTimestamp(string commandString)
        {
            commandString = commandString.Trim();

            if (commandString.Length == 14)
            {
                long result = 0;

                if (long.TryParse(commandString, out result))
                {
                    int year = 2000;
                    int month = 1;
                    int day = 1;
                    int hour = 1;
                    int minute = 0;
                    int second = 0;
                    DateTime dt = new DateTime(year, month, day, hour, minute, second);
                    return true;
                }
            }

            return false;
        }


        private string RemoveFromStartAndTrim(string stringToRemove,string stringToRemoveFrom)
        {
            stringToRemoveFrom = stringToRemoveFrom.Trim();
            stringToRemove = stringToRemove.Trim();
            stringToRemoveFrom = stringToRemoveFrom.Remove(0, stringToRemove.Length);
            return stringToRemoveFrom.Trim();
        }


     
       


      


        private string ProcessCreateCommand(string commandString)
        {
            if (commandString.StartsWith("database")) return ProcessCreateDatabaseCommand(MigrationConfiguration.DatabaseName, MigrationConfiguration.MasterConnectionString);

            if (commandString.StartsWith("schema")) return ProcessCreateSchemaCommand(MigrationConfiguration.ConnectionString);

            if (commandString.StartsWith("migration")) return ProcessCreateMigrationCommand(RemoveFromStartAndTrim("migration", commandString));

            return "command not available";
        }

       

       
   
      
        private string ProcessScriptCommand(string commandString)
        {
            if (commandString.Trim() == "") return "must specify a version or 'all'";
  
            if (commandString.StartsWith("all")) return ProcessScriptAllCommand();

            if (IsVerionTimestamp(commandString)) 
                return ProcessScriptVerionCommand(commandString.Trim());
            else
                return "Incorrect Timestamp " + commandString;
        }

        private string ProcessPrintCommand(string commandString)
        {
            if (commandString.Trim() == "") return "must specify a version or 'all'";

            if (commandString.StartsWith("all")) return ProcessPrintAllCommand();

            if (IsVerionTimestamp(commandString))
                return ProcessPrintVerionCommand(commandString.Trim());
            else
                return "Incorrect Timestamp " + commandString;
        }


       
        private string ProcessMigrateCommand(string commandString)
        {
            if (commandString.StartsWith("up")) return ProcessMigrateUpCommand(RemoveFromStartAndTrim("up", commandString));

            if (commandString.StartsWith("down")) return ProcessMigrateDownCommand(RemoveFromStartAndTrim("down", commandString));

            //if (commandString.StartsWith("to")) return ProcessMigrateToCommand(RemoveFromStartAndTrim("to", commandString));
            string version = RemoveFromStartAndTrim("to", commandString);
            if (commandString.StartsWith("to")) return ProcessMigrateTo(MigrationConfiguration.ConnectionString,RemoveFromStartAndTrim("to", version));

            return "command not available";
        }


       
    
        private string ProcessMigrateDownCommand(string commandString)
        {
            commandString = commandString.Trim();

            if (commandString == "all")
                return ProcessMigrateDownAllCommand(MigrationConfiguration.ConnectionString);
            else
                return ProcessMigrateDown(MigrationConfiguration.ConnectionString);
        }

      

        private string ProcessMigrateUpCommand(string commandString)
        {
            commandString = commandString.Trim();

            if (commandString == "all")
                return ProcessMigrateUpAllCommand(MigrationConfiguration.ConnectionString);
            else
                return ProcessMigrateUp(MigrationConfiguration.ConnectionString);
        }



        public string ProcessCreateDatabaseAndSchemaCommand(string databaseName, string connectionString, string masterConnectionString)
        {
            ProcessCreateDatabaseCommand(databaseName, masterConnectionString);
            ProcessCreateSchemaCommand(connectionString);
            return "command executed";

        }

      


        private string ProcessCreateDatabaseCommand(string databaseName, string masterConnectionString)
        {
            try
            {
                //SqlScriptExecuter.CreateDatabase(MigrationConfiguration.DatabaseName, MigrationConfiguration.MasterConnectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);
                SqlScriptExecuter.CreateDatabase(databaseName, masterConnectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);
                return "command executed";
            }
            catch (Exception ex)
            {
                return "error creating database. Check your database name and connection string and make sure the database has not been already created" + Environment.NewLine + ex.Message;
            }
        }

        private string ProcessCreateSchemaCommand(string connectionString)
        {
            try
            {
                string result = SqlScriptExecuter.CreateSchemaVersionTable(connectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);

                return result;
            }
            catch (Exception ex)
            {
                return "error accesssing the version table. Check your database name and connection string and make sure the database and version table have not been already created" + Environment.NewLine + ex.Message;
            }
        }

        private string ProcessVersionCommand(string connectionString)
        {
            try
            {
                return SqlScriptExecuter.GetSchemaVersion(connectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);
            }
            catch(Exception ex)
            {
                return "error accesssing the version table. Check your database name and connection string and make sure the database and version table have been created" + Environment.NewLine + ex.Message;
            }
        }


        private string ProcessCreateMigrationCommand(string migrationName)
        {

            if (string.IsNullOrEmpty(migrationName.Trim())) return "command is missing migration file name";

            MigrationFileCreator.CreateMigrationFile(MigrationConfiguration.migrationClassPath, migrationName.Trim().Replace(" ", "_"));

            return "Created a new migration file named " + migrationName.Trim().Replace(" ", "_") + ".cs in directory " + MigrationConfiguration.migrationClassPath;
        }

       

        private string ProcessScriptVerionCommand(string version)
        {
            try
            {
                string script = "";

                version = version.Trim();

                if (IsVerionTimestamp(version))
                {
                    string migrationFilePath = MigrationFactory.GetMigrationFilePathForVersion(version, MigrationConfiguration.migrationClassPath);


                    if (migrationFilePath != null)
                    {
                        MigrationFactory factory = MigrationFactory.GetFactoryForDatabaseType(MigrationConfiguration.ServerType);
                        Migration migration = factory.GetMigration(migrationFilePath, MigrationConfiguration.migrationLibraryAssemblyPath);

                        string[] parts = migrationFilePath.Split('\\');
                        string scriptFilePath = Path.Combine(MigrationConfiguration.migrationScriptPath, parts[parts.Length - 1]).Replace(".cs", ".sql");

                        script = "Generate change script version: " + version + Environment.NewLine + migration.Up(scriptFilePath);
                        script = script + Environment.NewLine + "Generate rollback script version: " + version + Environment.NewLine + migration.Down(scriptFilePath) + Environment.NewLine;
                        script = script + "Generated Script files are located in directory: " + MigrationConfiguration.migrationScriptPath;
                        return script;
                    }
                    else
                    {
                        return "migration file with the specified version does not exist. Command aborted.";
                    }
                }
                else
                {
                    return "Incorrect timestamp " + version;
                }
            }
            catch (Exception ex)
            {
                return "error accesssing the version table. Check your database name and connection string and make sure the database and version table have been created" + Environment.NewLine + ex.Message;
            }
        }

        private string ProcessScriptAllCommand()
        {
            try
            {
                string script = "";

                IEnumerable<string> filesPaths = MigrationFactory.GetMigrationFilePaths(MigrationConfiguration.migrationClassPath);

                MigrationFactory factory = MigrationFactory.GetFactoryForDatabaseType(MigrationConfiguration.ServerType);
                foreach (string migrationFilePath in filesPaths)
                {
                    Migration migration = factory.GetMigration(migrationFilePath, MigrationConfiguration.migrationLibraryAssemblyPath);

                    string version = MigrationFactory.GetVersionFromMigrationFilePath(migrationFilePath);
                    string[] parts = migrationFilePath.Split('\\');
                    string scriptFilePath = Path.Combine(MigrationConfiguration.migrationScriptPath, parts[parts.Length - 1]).Replace(".cs", ".sql");

                    script = script + "Generate change script version: " + version + Environment.NewLine + migration.Up(scriptFilePath);
                    script = script + Environment.NewLine + "Generate rollback script version: " + version + Environment.NewLine + migration.Down(scriptFilePath) + Environment.NewLine;
                }

                script = script + "Generated Script files are located in directory: " + MigrationConfiguration.migrationScriptPath;

                return script;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string ProcessPrintVerionCommand(string version)
        {
            try
            {
                string script = "";

                version = version.Trim();

                if (IsVerionTimestamp(version))
                {
                    string migrationFilePath = MigrationFactory.GetMigrationFilePathForVersion(version, MigrationConfiguration.migrationClassPath);


                    if (migrationFilePath != null)
                    {
                        MigrationFactory factory = MigrationFactory.GetFactoryForDatabaseType(MigrationConfiguration.ServerType);
                        Migration migration = factory.GetMigration(migrationFilePath, MigrationConfiguration.migrationLibraryAssemblyPath);


                        script = "Print change script version: " + version + Environment.NewLine + migration.Up();
                        script = script + Environment.NewLine + "Print rollback script version: " + version + Environment.NewLine + migration.Down() + Environment.NewLine;

                        return script;
                    }
                    else
                    {
                        return "migration file with the specified version does not exist. Command aborted.";
                    }
                }
                else
                {
                    return "Incorrect timestamp " + version;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        private string ProcessPrintAllCommand()
        {
            try
            {
                string script = "";

                IEnumerable<string> filesPaths = MigrationFactory.GetMigrationFilePaths(MigrationConfiguration.migrationClassPath);

                MigrationFactory factory = MigrationFactory.GetFactoryForDatabaseType(MigrationConfiguration.ServerType);
                foreach (string migrationFilePath in filesPaths)
                {
                    Migration migration = factory.GetMigration(migrationFilePath, MigrationConfiguration.migrationLibraryAssemblyPath);
                    string version = MigrationFactory.GetVersionFromMigrationFilePath(migrationFilePath);


                    script = script + "Print change script version: " + version + Environment.NewLine + migration.Up();
                    script = script + Environment.NewLine + "Print rollback script version: " + version + Environment.NewLine + migration.Down() + Environment.NewLine;
                }

                return script;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        private string ProcessMigrateUp(string connectionString)
        {
            try
            {
                string script = "";

                string currentVersionFromDatabase = SqlScriptExecuter.GetSchemaVersion(connectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);

                string nextVersion = GetNextVersion(currentVersionFromDatabase, MigrationConfiguration.migrationClassPath);

                if (nextVersion != null)
                {
                    string migrationFilePath = MigrationFactory.GetMigrationFilePathForVersion(nextVersion, MigrationConfiguration.migrationClassPath);


                    if (migrationFilePath != null)
                    {
                        MigrationFactory factory = MigrationFactory.GetFactoryForDatabaseType(MigrationConfiguration.ServerType);

                        Migration migration = factory.GetMigration(migrationFilePath, MigrationConfiguration.migrationLibraryAssemblyPath);

                        string version = MigrationFactory.GetVersionFromMigrationFilePath(migrationFilePath);

                        DatabaseConnection db = new DatabaseConnection(MigrationConfiguration.ConnectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);

                        string executedMigration = "Executed Migration Version " + version + " Up " + Environment.NewLine;

                        try
                        {
                            script = migration.Up(db);
                        }
                        catch (Exception ex)
                        {
                            return "Error:" + ex.Message;
                        }


                        try
                        {
                            SqlScriptExecuter.UpdateSchemaVersion(nextVersion, MigrationConfiguration.ConnectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);
                        }
                        catch
                        {
                            return "Error: changed from current database version " + currentVersionFromDatabase + " but unable to set next version to " + nextVersion;
                        }

                        return executedMigration + script.Replace("begin transaction", "").Replace("commit transaction", "") + Environment.NewLine + "current version is: " + nextVersion;
                    }
                    else
                    {
                        return "Error: migration file with the specified version does not exist. Command aborted. - current database version is: " + currentVersionFromDatabase;
                    }
                }
                else
                {
                    return "Done: Next version does not exist - current database version is: " + currentVersionFromDatabase;
                }
            }
            catch (Exception ex)
            {
                return "Error:" + Environment.NewLine + ex.Message;
            }     
        }

        private string ProcessMigrateDown(string connectionString)
        {
            try
            {

                string script = "";

                string currentVersionFromDatabase = SqlScriptExecuter.GetSchemaVersion(connectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);

                if (currentVersionFromDatabase != "00000000000000")
                {
                    string migrationFilePath = MigrationFactory.GetMigrationFilePathForVersion(currentVersionFromDatabase, MigrationConfiguration.migrationClassPath);

                    string prevVersion = GetPreviousVersion(currentVersionFromDatabase, MigrationConfiguration.migrationClassPath);

                    if (migrationFilePath != null)
                    {
                        MigrationFactory factory = MigrationFactory.GetFactoryForDatabaseType(MigrationConfiguration.ServerType);

                        Migration migration = factory.GetMigration(migrationFilePath, MigrationConfiguration.migrationLibraryAssemblyPath);

                        string version = MigrationFactory.GetVersionFromMigrationFilePath(migrationFilePath);

                        DatabaseConnection db = new DatabaseConnection(MigrationConfiguration.ConnectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);

                        string executedMigration = "Executed Migration Version " + version + " Down " + Environment.NewLine;

                        try
                        {
                            script = migration.Down(db);
                        }
                        catch (Exception ex)
                        {
                            return "Error:" + ex.Message;
                        }


                        try
                        {
                            SqlScriptExecuter.UpdateSchemaVersion(prevVersion, MigrationConfiguration.ConnectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);
                        }
                        catch
                        {
                            return "Error: rolled back from version " + currentVersionFromDatabase + " but unable to set previous version to " + prevVersion;
                        }

                        return executedMigration + script.Replace("begin transaction", "").Replace("commit transaction", "") + Environment.NewLine + "current version is: " + prevVersion;
                    }
                    else
                    {
                        return "Error: migration file with the specified version does not exist. Command aborted. - current database version is: " + currentVersionFromDatabase;
                    }
                }
                else
                {
                    return "Done: Prev version does not exist - current database version : " + currentVersionFromDatabase;
                }
            }
            catch (Exception ex)
            {
                return "Error:" + Environment.NewLine + ex.Message;
            }    
        }

        private string ProcessMigrateUpAllCommand(string connectionString)
        {
            string accscript = ProcessMigrateUp(connectionString);
            string script = accscript;
            while (!accscript.StartsWith("Error:") && !accscript.StartsWith("Done:"))
            {
                accscript = ProcessMigrateUp(connectionString);

                script = script + Environment.NewLine + accscript;
            }
            return script;
        }

      

        private string ProcessMigrateDownAllCommand(string connectionString)
        {
            string accscript = ProcessMigrateDown(connectionString);
            string script = accscript;
            while (!accscript.StartsWith("Error:") && !accscript.StartsWith("Done:"))
            {
                accscript = ProcessMigrateDown(connectionString);

                script = script + Environment.NewLine + accscript;          
            }
            return script;
        }


        private string ProcessMigrateTo(string connectionString,string version)
        {

            try
            {
                version = version.Trim();

                if (version == "0") version = "00000000000000";

                if (IsVerionTimestamp(version))
                {
                    string currentVersionFromDatabase = SqlScriptExecuter.GetSchemaVersion(MigrationConfiguration.ConnectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);

                    if (currentVersionFromDatabase != version)
                    {
                        if (version == "00000000000000")
                        {
                            return ProcessMigrateDownAllCommand(connectionString);
                        }
                        else
                        {
                            string file = MigrationFactory.GetMigrationFilePathForVersion(version, MigrationConfiguration.migrationClassPath);

                            if (file != null)
                            {
                                if (long.Parse(currentVersionFromDatabase) < long.Parse(version))
                                {
                                    return ProcessMigrateUpTo(connectionString,currentVersionFromDatabase, version);
                                }
                                else
                                {
                                    return ProcessMigrateDownTo(connectionString,currentVersionFromDatabase, version);
                                }
                            }
                        }
                    }

                    return version;
                }
                else
                {
                    return "Incorrect timestamp " + version;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }   
        }



        private string ProcessMigrateUpTo(string connectionString, string currentVersionFromDatabase, string version)
        {
            string accscript = "";
            string script = "";
            while (long.Parse(currentVersionFromDatabase) < long.Parse(version))
            {
                accscript = ProcessMigrateUp(connectionString);

                script = script + Environment.NewLine + accscript;

                currentVersionFromDatabase = SqlScriptExecuter.GetSchemaVersion(connectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);
            }
            return script;
        }

        private string ProcessMigrateDownTo(string connectionString, string currentVersionFromDatabase, string version)
        {
            string accscript = "";
            string script = "";
            while (long.Parse(currentVersionFromDatabase) > long.Parse(version))
            {
                accscript = ProcessMigrateDown(connectionString);

                script = script + Environment.NewLine + accscript;

                currentVersionFromDatabase = SqlScriptExecuter.GetSchemaVersion(connectionString, MigrationConfiguration.ProviderName, MigrationConfiguration.ServerType);

            }
            return script;
        }

        string GetNextVersion(string version, string migrationClassFileDirectory)
        {
            SortedList list = MigrationFactory.GetSortedMigrationFileVersionsFromMigrationFiles(migrationClassFileDirectory);

            int index = list.IndexOfKey(version);

            string nextversion = null;

            if (index < list.Count - 1)
                nextversion = list.GetByIndex(index + 1).ToString();

            return nextversion;
        }

        string GetPreviousVersion(string version, string migrationClassFileDirectory)
        {
            SortedList list = MigrationFactory.GetSortedMigrationFileVersionsFromMigrationFiles(migrationClassFileDirectory);

            int index = list.IndexOfKey(version);

            string previousversion = "00000000000000";

            if (index > 0)
                previousversion = list.GetByIndex(index - 1).ToString();

            return previousversion;
        }


    }
}
