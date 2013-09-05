using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class MigrateCommand : IDatabaseCommand
    {
        public static string CommandSyntax = "migrate <database> to run next up migration or migrate <database> all up migrations";

        public void Process(string[] args)
        {
            string databaseName = args[0];

            string connectionString = MigrationConfiguration.ConnectionStringFor(databaseName);

            if (args.Length == 3)
            {               
                string result = new MigrationCommands().ProcessMigrateUpAllCommand(connectionString);
                Console.WriteLine(result);

            }
            else if (args.Length == 2)
            {
                string result = new MigrationCommands().ProcessMigrateUp(connectionString);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine(SetupCommand.CommandSyntax);
            }

        }
    }
}
