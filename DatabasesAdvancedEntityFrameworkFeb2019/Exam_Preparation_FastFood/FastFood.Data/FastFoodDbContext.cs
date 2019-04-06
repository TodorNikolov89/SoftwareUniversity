using System;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    public class FastFoodDbContext : DbContext
    {
        public FastFoodDbContext()
        {
        }

        public FastFoodDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EmployeesConfiguration(builder);
            PositionsConfiguration(builder);
            CategoriesConfiguration(builder);
            OrderItemsConfiguration(builder);
            ItemsItemsConfiguration(builder);

        }

        private void ItemsItemsConfiguration(ModelBuilder builder)
        {
            builder
             .Entity<Item>()
             .HasIndex(n => n.Name)
             .IsUnique();
        }

        private void OrderItemsConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<OrderItem>()
                .HasKey(pk => new { pk.OrderId, pk.ItemId });

            builder
                .Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(fk => fk.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<OrderItem>()
                .HasOne(o => o.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(fk => fk.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void CategoriesConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<Category>()
                .HasMany(o => o.Items)
                .WithOne(e => e.Category)
                .HasForeignKey(fk => fk.CategoryId);
        }

        private void PositionsConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<Position>()
                .HasMany(o => o.Employees)
                .WithOne(e => e.Position)
                .HasForeignKey(fk => fk.PositionId);

            builder
                .Entity<Position>()
                .HasIndex(n => n.Name)
                .IsUnique();
        }

        private void EmployeesConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .HasMany(o => o.Orders)
                .WithOne(e => e.Employee)
                .HasForeignKey(fk => fk.EmployeeId);
        }
    }
}