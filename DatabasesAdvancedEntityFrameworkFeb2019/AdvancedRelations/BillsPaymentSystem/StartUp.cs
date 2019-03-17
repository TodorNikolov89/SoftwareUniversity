using BillsPaymentSystem.Core;
using BillsPaymentSystem.Core.Contracts;
using BillsPaymentSystem.Data;
using System;

namespace BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ICommandInterpreter commandInterpreter = new CommandInterpreter();

            IEngine engine = new Engine(commandInterpreter);
            engine.Run();
                
        }
    }
}
