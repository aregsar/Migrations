using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Migrations;
using mc.Commands;

namespace mc
{
    class Program
    {
        private static Dictionary<string, IDatabaseCommand> Commands = new Dictionary<string, IDatabaseCommand>();

        static void Main(string[] args)
        {
            try
            {
                LoadCommands();

                //RunTests("migration");//TEMP

                if (args.Length > 0)
                {
                    ProcessCommandLineArguments(args);
                }
                else
                {
                    StartCommandProcessingLoop();                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press a key to shut down the program");
                Console.ReadLine();
            }
        }
        
        private static void LoadCommands()
        {
            Commands.Add("commands", new CommandsCommand());

            Commands.Add("setup", new SetupCommand());

            Commands.Add("update", new MigrateCommand());

            Commands.Add("rollback", new RollbackCommand());

            Commands.Add("script", new ScriptCommand());

            Commands.Add("version", new VersionCommand());

        }

        private static void StartCommandProcessingLoop()
        {
            Console.WriteLine(">type help for available commands. type exit to exit console.");

            while (true)
            {
                Console.Write(">");

                string input = Console.ReadLine().Trim().ToLower();

                if (input.Trim().StartsWith("exit")) break;

                if (input != String.Empty)
                {
                    if (input == "v") input = "version";

                    Console.WriteLine(new MigrationCommands().ProcessCommand(input));
                }
            }
        }

      


        private static void ProcessCommandLineArguments(string[] args)
        {
          
          
            string command = args[0];

            IDatabaseCommand c  = Commands[command];

            c.Process(args);
 
        }

        private static void RunTests(string database)
        {
            string[] pars = new string[] { "version", database };
            new VersionCommand().Process(pars);

            pars = new string[] { "update", database };
            new MigrateCommand().Process(pars);

            pars = new string[] { "rollback", database };
            new RollbackCommand().Process(pars);

            pars = new string[] { "update", database , "all" };
            new MigrateCommand().Process(pars);

            pars = new string[] { "rollback", database, "all" };
            new RollbackCommand().Process(pars);

            pars = new string[] { "version", database, "20130903112233" };
            new VersionCommand().Process(pars);

            pars = new string[] { "version", database, "0" };
            new VersionCommand().Process(pars);

            pars = new string[] { "script"};
            new ScriptCommand().Process(pars);

        }


 

    }
}
