using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class VersionCommand : IDatabaseCommand
    {
        public static string CommandSyntax = "version <database> to show current version  or version <database> <version> to migrate to specific version";

        public void Process(string[] args)
        {
            string databaseName = args[0];

            string connectionString = MigrationConfiguration.ConnectionStringFor(databaseName);

            if (args.Length == 3)
            {
                
                string version = args[2];

                string result = new MigrationCommands().ProcessMigrateTo(connectionString, version);
                Console.WriteLine(result);
                
            }
            else if (args.Length == 2)
            {

                string result = new MigrationCommands().ProcessVersionCommand(connectionString);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine(SetupCommand.CommandSyntax);
            }


        }
    }
}
