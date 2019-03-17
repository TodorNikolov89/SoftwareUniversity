using BillsPaymentSystem.Core.Commands.Contracts;
using BillsPaymentSystem.Core.Contracts;
using BillsPaymentSystem.Data;
using System;
using System.Linq;
using System.Reflection;

namespace BillsPaymentSystem.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";

        public string Read(string[] args, BillsPaymentSystemContext context)
        {
            string command = args[0];
            string[] commandArguments = args.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == command + Suffix);

            if (type == null)
            {
                throw new ArgumentNullException("Command not found!");
            }

            var typeInstance = Activator.CreateInstance(type, context);

            var result = ((ICommand)typeInstance).Execute(commandArguments);

            return result;
        }
    }
}
