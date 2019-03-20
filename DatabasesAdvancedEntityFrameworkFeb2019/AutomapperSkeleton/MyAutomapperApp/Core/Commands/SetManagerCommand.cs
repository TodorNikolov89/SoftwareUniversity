namespace MyAutomapperApp.Core.Commands
{
    using AutoMapper;
    using MyAutomapperApp.Core.Commands.Contracts;
    using MyAutomapperApp.Data;

    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetManagerCommand(MyAppContext context)
        {
            this.context = context;
        }

        public string Execute(string[] inputArgs)
        {
            int emplooyeeId = int.Parse(inputArgs[0]);
            int managerId = int.Parse(inputArgs[1]);

            var employee = this.context.Employees.Find(emplooyeeId);
            var manager = this.context.Employees.Find(managerId);

            employee.Manager = manager;

            this.context.SaveChanges();

            return $"Command completed successfully!";


        }
    }
}
