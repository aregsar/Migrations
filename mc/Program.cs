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
            //new MigrationCommands().ProcessCommand("migrate to " + version);    
            //mc.exe args
            //mc.exe setup catapult
            //mc.exe migrate catapult
            //mc.exe migrate catapult all
            //mc.exe migrate catapult 1

            //mc.exe migrate catapult
            //mc.exe migrate catapult all

          
            string command = args[0];

            IDatabaseCommand c  = Commands[command];

            c.Process(args);
 
        }

        


 

    }
}
