using System;
using System.Collections.Generic;
using System.Text;

namespace MyAutomapperApp.Core.Commands.Contracts
{
    interface ICommand
    {
        string Execute(string[] inputArgs);
    }
}
