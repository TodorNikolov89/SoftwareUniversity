namespace MyAutomapperApp.Core.Commands
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MyAutomapperApp.Core.Commands.Contracts;
    using MyAutomapperApp.Core.ViewModels;
    using MyAutomapperApp.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class ManagerInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ManagerInfoCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var allEmployees = context.Employees.ToList();

            var managerId = int.Parse(inputArgs[0]);

            var manager = this.context
                .Employees
                .Include(m => m.ManagedEmployees)
                .FirstOrDefault(x => x.Id == managerId);
            ;
            var managerDto = this.mapper.CreateMappedObject<ManagerDto>(manager);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.ManagedEmployees.Count}");


            foreach (var emp in managerDto.ManagedEmployees)
            {
                sb.AppendLine($"- {managerDto.FirstName} {managerDto.LastName} - {manager.Salary}");

            }
            return sb.ToString().TrimEnd();
        }
    }
}
