using AutoMapper;
using MyAutomapperApp.Core.Commands.Contracts;
using MyAutomapperApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAutomapperApp.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeePersonalInfoCommand(MyAppContext context, Mapper mapper)
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

            StringBuilder sb = new StringBuilder();

            

            sb.AppendLine($"ID: {employee.Id} - {employee.FirstName + " " + employee.LastName} - ${employee.Salary:f2}");
            sb.AppendLine($"Birthday: {employee.Birthday}");
            sb.AppendLine($"Address: {employee.Address}");

            string result = sb.ToString().TrimEnd();

            return result;
        }
    }
}
