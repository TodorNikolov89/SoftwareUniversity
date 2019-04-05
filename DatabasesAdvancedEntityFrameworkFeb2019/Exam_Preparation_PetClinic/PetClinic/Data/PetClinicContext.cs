namespace PetClinic.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            : base(options) { }


        public DbSet<Animal> Animals { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            AnimalConfiguration(builder);
            VetConfiguration(builder);
            ProcedureAnimalAidConfiguration(builder);
            AnimalAidConfiguration(builder);
        }

        private void AnimalAidConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<AnimalAid>()
                .HasIndex(n => n.Name)
                .IsUnique();
        }

        private void ProcedureAnimalAidConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<ProcedureAnimalAid>()
                .HasKey(pk => new { pk.ProcedureId, pk.AnimalAidId });

            builder
                .Entity<ProcedureAnimalAid>()
                .HasOne(p => p.Procedure)
                .WithMany(a => a.ProcedureAnimalAids)
                .HasForeignKey(fk => fk.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<ProcedureAnimalAid>()
               .HasOne(p => p.AnimalAid)
               .WithMany(a => a.AnimalAidProcedures)
               .HasForeignKey(fk => fk.AnimalAidId)
               .OnDelete(DeleteBehavior.Restrict);
        }

        private void VetConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<Vet>()
                .HasMany(p => p.Procedures)
                .WithOne(v => v.Vet)
                .HasForeignKey(fk => fk.VetId);

            builder
                .Entity<Vet>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();
        }

        private void AnimalConfiguration(ModelBuilder builder)
        {
            builder
                .Entity<Animal>()
                .HasOne(p => p.Passport)
                .WithOne(a => a.Animal);

            //builder
            //    .Entity<Animal>()
            //    .HasMany(p => Procedures)
            //    .WithOne(a => a.Animal)
            //    .HasForeignKey(fk => fk.AnimalId);
        }
    }
}
