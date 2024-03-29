﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class SetupCommand : IDatabaseCommand
    {
        public static string CommandSyntax = "setup <database>";
        public void Process(string[] args)
        {
            if (args.Length > 1)
            {
                               
                string databaseName = args[0];

                string connectionString = MigrationConfiguration.ConnectionStringFor(databaseName);

                string result = new MigrationCommands().ProcessCreateDatabaseAndSchemaCommand(databaseName, connectionString, MigrationConfiguration.MasterConnectionString);

            }
            else
            {
                Console.WriteLine("invalid arguments to setup command");
                Console.WriteLine("Command syntax is:");
                Console.WriteLine(CommandSyntax);

            }

        }
    }
}
