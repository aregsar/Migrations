using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrations;

namespace mc
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                Console.WriteLine(">type help for available commands. type exit to exit console.");

                //Console.WriteLine(new MigrationCommands().ProcessCommand("version"));

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

                    //if (input == String.Empty)
                    //{
                    //    Console.WriteLine(new MigrationCommands().ProcessCommand("version"));
                    //}
                    //else
                    //{
                    //    Console.WriteLine(new MigrationCommands().ProcessCommand(input));
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press a key to shut down the program");
                Console.ReadLine();
            }
        }


    }
}
