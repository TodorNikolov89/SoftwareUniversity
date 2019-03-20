using Microsoft.Extensions.DependencyInjection;
using MyAutomapperApp.Core.Commands.Contracts;
using MyAutomapperApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyAutomapperApp.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {

        private const string Suffix = "Command";
        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider provider)
        {
            this.serviceProvider = provider;
        }

        public string Read(string[] inputArgs)
        {
            string commandName = inputArgs[0] + Suffix;

            string[] commandParams = inputArgs.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                throw new ArgumentNullException("Invalid command!");
            }

            var constructor = type.GetConstructors()
                .FirstOrDefault();

            var constructorParams = constructor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var services = constructorParams
                .Select(this.serviceProvider.GetService)
                .ToArray();
                ;
            var command = (ICommand)constructor
                .Invoke(services);


            string result = command.Execute(commandParams);
            return result;
        }
    }//AddEmployee Pesho Petrov 3333
}
