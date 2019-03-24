using AutoMapper;
using MyAutomapperApp.Core.Commands.Contracts;
using MyAutomapperApp.Core.ViewModels;
using MyAutomapperApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAutomapperApp.Core.Commands
{
    public class SetAddressCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetAddressCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            string address = inputArgs[1];

            var employee = context
                .Employees
                .FirstOrDefault(e => e.Id == employeeId);

            employee.Address = address;
            context.SaveChanges();

            string result = $"Employee address is set";

            return result;
        }
    }
}
