using BillsPaymentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] args, BillsPaymentSystemContext context);
    }
}
