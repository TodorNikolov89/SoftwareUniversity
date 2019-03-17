using BillsPaymentSystem.Core.Contracts;
using BillsPaymentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            while (true)
            {
                string[] inputParams = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string result = string.Empty;

                using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
                {
                     result = this.commandInterpreter.Read(inputParams, context);
                }

                Console.WriteLine(result);

            }
        }
    }
}
