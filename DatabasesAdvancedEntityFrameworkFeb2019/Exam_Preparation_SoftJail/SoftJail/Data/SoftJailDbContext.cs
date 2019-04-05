namespace SoftJail.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using SoftJail.Data.Models;

    public class SoftJailDbContext : DbContext
    {
        public SoftJailDbContext()
        {
        }

        public SoftJailDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Cell> Cells { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; }
        public DbSet<Prisoner> Prisoners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            CellCongiguration(builder);
            DepartmentCongiguration(builder);
            PrisonerCongiguration(builder);
            OfficersPrisonersCongiguration(builder);
        }

        private void OfficersPrisonersCongiguration(ModelBuilder builder)
        {
            builder
                 .Entity<OfficerPrisoner>()
                 .HasKey(pk => new { pk.PrisonerId, pk.OfficerId });

            builder
                .Entity<OfficerPrisoner>()
                .HasOne(o => o.Officer)
                .WithMany(p => p.OfficerPrisoners)
                .HasForeignKey(fk => fk.OfficerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<OfficerPrisoner>()
                .HasOne(p => p.Prisoner)
                .WithMany(o => o.PrisonerOfficers)
                .HasForeignKey(fk => fk.PrisonerId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

        private void PrisonerCongiguration(ModelBuilder builder)
        {
            builder
                .Entity<Prisoner>()
                .HasMany(m => m.Mails)
                .WithOne(p => p.Prisoner)
                .HasForeignKey(fk => fk.PrisonerId);
            

        }

        private void DepartmentCongiguration(ModelBuilder builder)
        {
            builder
                .Entity<Department>()
                .HasMany(c => c.Cells)
                .WithOne(d => d.Department)
                .HasForeignKey(fk => fk.DepartmentId);
        }

        private void CellCongiguration(ModelBuilder builder)
        {
            builder
                .Entity<Cell>()
                .HasMany(p => p.Prisoners)
                .WithOne(c => c.Cell)
                .HasForeignKey(fk => fk.CellId);

        }
    }
}