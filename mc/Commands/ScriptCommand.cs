using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class ScriptCommand : IDatabaseCommand
    {


        public void Process(string[] args)
        {
            string databaseName = args[0];

            // MigrationFactory.GetMigrationFilePaths(MigrationConfiguration.migrationClassPath);
            string result = new MigrationCommands().ProcessScriptAllCommand(databaseName);
          
            Console.WriteLine("generated all migration scripts in directory " + MigrationConfiguration.migrationScriptPath + "\\" + databaseName);
            Console.WriteLine("From migration Source files at " + MigrationConfiguration.migrationClassPath);
     

        }
    }
}
