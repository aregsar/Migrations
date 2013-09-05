using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class DropCommand : IDatabaseCommand
    {

        public void Process(string[] args)
        {
            string databaseName = args[0];

            string connectionString = MigrationConfiguration.ConnectionStringFor(databaseName);

            Console.WriteLine(SetupCommand.CommandSyntax);

        }
    }
}
