using AutoMapper;
using MyAutomapperApp.Core.Commands.Contracts;
using MyAutomapperApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAutomapperApp.Core.Commands
{
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ListEmployeesOlderThanCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeAge = int.Parse(inputArgs[0]);

            return "";
        }
    }
}
