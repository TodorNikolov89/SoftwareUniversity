using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                var result = RemoveTown(context);

                Console.WriteLine(result);
            }

        }

        //3.	Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary,
                    e.EmployeeId
                })
                .OrderBy(x => x.EmployeeId).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            string result = sb.ToString().TrimEnd();
            return result;
        }

        //4.	Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        //5.	Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary:f2}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        //6.	Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address()
            {
                TownId = 4,
                AddressText = "Vitoshka 15"
            };

            var employee = context.Employees
                .Where(e => e.LastName == "Nakov").FirstOrDefault();

            context.Addresses.Add(address);

            employee.Address = address;
            context.SaveChanges();


            var employees = context.Employees
                .Select(e => new
                {
                    e.Address.AddressText,
                    e.AddressId
                })
                .OrderByDescending(e => e.AddressId).Take(10);

            StringBuilder sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.AddressText}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        //7.	Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    EmployeeFullName = e.FirstName + " " + e.LastName,
                    ManagerFullName = e.Manager.FirstName + " " + e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        StartDate = p.Project.StartDate,
                        EndDate = p.Project.EndDate
                    }).ToList()
                })
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.EmployeeFullName} - Manager: {employee.ManagerFullName}");

                foreach (var proj in employee.Projects)
                {
                    var startDate = proj.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    var endDate = proj.EndDate.HasValue ?
                        proj.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished";


                    sb.AppendLine($"--{proj.ProjectName} - {startDate} - {endDate}");
                }
            }
            string result = sb.ToString().TrimEnd();

            return result;
        }

        //8.	Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Select(ad => new
                {
                    ad.AddressText,
                    ad.Town,
                    ad.Employees
                })
                .OrderByDescending(a => a.Employees.Count())
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();


            StringBuilder sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count()} employees");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        //9.	Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                 .Where(e => e.EmployeeId == 147)
                 .Select(e => new
                 {
                     FullName = e.FirstName + " " + e.LastName,
                     e.JobTitle,
                     Projects = e.EmployeesProjects
                     .Where(em => em.EmployeeId == 147)
                     .Select(pn => pn.Project.Name)
                     .ToList(),
                 })
                 .ToList();



            StringBuilder sb = new StringBuilder();

            foreach (var emp in employee)
            {
                sb.AppendLine($"{emp.FullName} - {emp.JobTitle}");

                foreach (var p in emp.Projects.OrderBy(p => p))
                {
                    sb.AppendLine($"{p}");
                }
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        //10.	Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Select(d => new
                {
                    d.Name,
                    ManagerFullName = d.Manager.FirstName + " " + d.Manager.LastName,
                    Employees = d.Employees
                    .Where(e => e.DepartmentId == d.DepartmentId)
                    .ToList()
                })
                .Where(a => a.Employees.Count > 5)
                .OrderBy(c => c.Employees.Count)
                .ThenBy(dn => dn.Name)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var dep in departments)
            {
                sb.AppendLine($"{dep.Name} – {dep.ManagerFullName}");

                foreach (var emp in dep.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    string fullName = emp.FirstName + " " + emp.LastName;
                    sb.AppendLine($"{fullName} - {emp.JobTitle}");
                }
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        //11.	Find Latest 10 Projects
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .OrderByDescending(p => p.StartDate)
                .ToList()
                .Take(10);

            StringBuilder sb = new StringBuilder();

            foreach (var proj in projects.OrderBy(p => p.Name))
            {
                sb.AppendLine($"{proj.Name}");
                sb.AppendLine($"{proj.Description}");
                sb.AppendLine($"{proj.StartDate}");
            }

            string result = sb.ToString().TrimEnd();
            return result;
        }

        //12.	Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services")
                .OrderBy(n => n.FirstName)
                .ThenBy(n => n.LastName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                employee.Salary *= 1.12m;
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }
            context.SaveChanges();
            string result = sb.ToString().TrimEnd();

            return result;
        }

        //13.	Find Employees by First Name Starting With "Sa"
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => EF.Functions.Like(e.FirstName, "sa%"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var em in employees)
            {
                sb.AppendLine($"{em.FirstName} {em.LastName} - {em.JobTitle} - (${em.Salary:f2})");
            }

            string result = sb.ToString().TrimEnd();
            return result;
        }

        //14.	Delete Project by Id
        public static string DeleteProjectById(SoftUniContext context)
        {

            var project = context.Projects.FirstOrDefault(p => p.ProjectId == 2);
            var empProjects = context.EmployeesProjects.Where(ep => ep.ProjectId == 2).ToList();

            context.Projects.Remove(project);
            context.EmployeesProjects.RemoveRange(empProjects);

            context.SaveChanges();

            var proj = context.Projects.Select(x => x.Name).ToList().Take(10);

            StringBuilder sb = new StringBuilder();

            foreach (var p in proj)
            {
                sb.AppendLine($"{p}");
            }

            string result = sb.ToString().TrimEnd();

            return result;
        }

        //15.	Remove Town
        public static string RemoveTown(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Address.Town.Name == "Seattle").ToList();

            foreach (var emp in employees)
            {
                emp.AddressId = null;
            }

            var addresses = context.Addresses
                .Where(a => a.Town.Name == "Seattle").ToList();
            
            int addressesCount = context.Addresses
                    .Where(a => a.Town.Name == "Seattle")
                    .Count();



            context.Addresses
                    .Where(a => a.Town.Name == "Seattle")
                    .ToList()
                    .ForEach(a => context.Addresses.Remove(a));

            context.Towns
                    .Remove(context.Towns
                        .SingleOrDefault(t => t.Name == "Seattle"));

            context.SaveChanges();

            string result = $"{addressesCount} {(addressesCount == 1 ? "address" : "addresses")} in Seattle {(addressesCount == 1 ? "was" : "were")} deleted";

            return result;
        }
    }
}
