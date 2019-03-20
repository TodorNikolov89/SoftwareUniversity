using System;
using System.Collections.Generic;
using System.Text;

namespace MyAutomapperApp.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] inputArgs);
    }
}
