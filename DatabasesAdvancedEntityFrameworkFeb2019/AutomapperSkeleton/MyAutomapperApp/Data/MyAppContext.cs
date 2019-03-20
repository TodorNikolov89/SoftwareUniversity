namespace MyAutomapperApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyAutomapperApp.Models;

    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Emlpoyee> Employees { get; set; }

    }
}
