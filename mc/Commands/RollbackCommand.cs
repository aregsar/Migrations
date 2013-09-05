using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class RollbackCommand : IDatabaseCommand
    {

        public static string CommandSyntax = "rollback <database> to run next down migration or rollback <database> all down migrations";

        public void Process(string[] args)
        {
            string databaseName = args[1];

            string connectionString = MigrationConfiguration.ConnectionStringFor(databaseName);

            if (args.Length == 3)
            {
                string result = new MigrationCommands().ProcessMigrateDownAllCommand(connectionString);
                Console.WriteLine(result);

            }
            if (args.Length == 2)
            {
                string result = new MigrationCommands().ProcessMigrateDown(connectionString);
                Console.WriteLine(result);

            }
            else
            {
                Console.WriteLine(SetupCommand.CommandSyntax);
            }

        }
    }
}
