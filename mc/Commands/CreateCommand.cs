using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class CreateCommand : IDatabaseCommand
    {

        public void Process(string[] args)
        {
            string databaseName = args[1];

            Console.WriteLine(SetupCommand.CommandSyntax);

        }
    }
}
