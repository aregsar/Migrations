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
            // MigrationFactory.GetMigrationFilePaths(MigrationConfiguration.migrationClassPath);
            string result = new MigrationCommands().ProcessScriptAllCommand();
          
            Console.WriteLine("generated all migration scripts in directory " + MigrationConfiguration.migrationScriptPath);
            Console.WriteLine("From migration Source files at " + MigrationConfiguration.migrationClassPath);
     

        }
    }
}
