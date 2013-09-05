using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class TruncateCommand : IDatabaseCommand
    {

        public void Process(string[] args)
        {
            string databaseName = args[1];

            string[] parts = databaseName.Split(new char[] { '.' });

            string database = parts[0];

            string connectionString = MigrationConfiguration.ConnectionStringFor(database);

            string table = parts[1];

            Console.WriteLine(SetupCommand.CommandSyntax);

        }
    }
}
