namespace Cinema.Data
{
    using System;
    using Cinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CinemaContext : DbContext
    {
        public CinemaContext() { }

        public CinemaContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            HallsConfiguration(modelBuilder);
            ProjectionsConfiguration(modelBuilder);
            CustomersConfiguration(modelBuilder);
            MoviesConfiguration(modelBuilder);
        }

        private void MoviesConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Movie>()
              .HasMany(t => t.Projections)
              .WithOne(p => p.Movie)
              .HasForeignKey(fk => fk.MovieId);
        }

        private void CustomersConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Customer>()
               .HasMany(t => t.Tickets)
               .WithOne(p => p.Customer)
               .HasForeignKey(fk => fk.CustomerId);
        }

        private void ProjectionsConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Projection>()
                .HasMany(t => t.Tickets)
                .WithOne(p => p.Projection)
                .HasForeignKey(fk => fk.ProjectionId);
        }

        private void HallsConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Hall>()
                .HasMany(s => s.Seats)
                .WithOne(h => h.Hall)
                .HasForeignKey(fk => fk.HallId);

            modelBuilder
                .Entity<Hall>()
                .HasMany(p => p.Projections)
                .WithOne(h => h.Hall)
                .HasForeignKey(fk => fk.HallId);
        }
    }
}