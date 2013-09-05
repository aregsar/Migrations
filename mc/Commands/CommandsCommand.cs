using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mc;

namespace mc.Commands
{
    public class CommandsCommand : IDatabaseCommand
    {
        //mc commands
        //mc setup catapult
        //mc migrate catapult
        //mc migrate catapult all

        //mc migrate catapult
        //mc migrate catapult all

        //mc version catapult
        //mc Version catapult 2012090613043255

        //mc script
       
        
        public void Process(string[] args)
        {
            
              Console.WriteLine(SetupCommand.CommandSyntax);

        }
    }
}
