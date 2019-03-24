using MyAutomapperApp.Core.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MyAutomapperApp.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Environment.Exit(0);
            string result = $"End";
            return result;
            //TODO exit method
            
        }
    }
}
