using AutoMapper;
using MyAutomapperApp.Core.Commands.Contracts;
using MyAutomapperApp.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyAutomapperApp.Core.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetBirthdayCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            int employeeId = int.Parse(inputArgs[0]);
            DateTime birthday = DateTime.ParseExact(inputArgs[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var employee = context.Employees.FirstOrDefault(e => e.Id == employeeId);

            employee.Birthday = birthday;
            context.SaveChanges();

            string result = $"Birthday date is set";
           

            return result;
        }
    }
}
