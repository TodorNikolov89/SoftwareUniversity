using AutoMapper;
using MyAutomapperApp.Core.Commands.Contracts;
using MyAutomapperApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAutomapperApp.Core.Commands
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeeInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);

            var employee = context
                .Employees
                .FirstOrDefault(e => e.Id == employeeId);
            

            string result = $"ID: {employee.Id} - {employee.FirstName} {employee.LastName} -  ${employee.Salary:f2}";

            return result;
        }
    }
}
