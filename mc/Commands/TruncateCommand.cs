﻿using System;
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

            Console.WriteLine(SetupCommand.CommandSyntax);

        }
    }
}
