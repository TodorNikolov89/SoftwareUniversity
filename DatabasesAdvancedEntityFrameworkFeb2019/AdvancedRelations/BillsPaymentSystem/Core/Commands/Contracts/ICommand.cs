using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Core.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
