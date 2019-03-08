namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using System;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.stringConfiguration);
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCourseEntity(modelBuilder);

            ConfigureStudentEntity(modelBuilder);

            ConfigureResourceEntity(modelBuilder);

            ConfigureHomeworkEntity(modelBuilder);

            ConfigureStudentsCourcesEntity(modelBuilder);

        }

        private void ConfigureStudentsCourcesEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<StudentCourse>()
                .HasKey(k => new
                {
                    k.StudentId,
                    k.CourseId
                });

            modelBuilder
                .Entity<StudentCourse>()
                .HasOne(c => c.Course)
                .WithMany(s => s.StudentsEnrolled)
                .HasForeignKey(fk => fk.CourseId);

            modelBuilder
                .Entity<StudentCourse>()
                .HasOne(s => s.Student)
                .WithMany(c => c.CourseEnrollments)
                .HasForeignKey(fk => fk.StudentId);
        }

        private void ConfigureHomeworkEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Homework>()
                .HasKey(k => k.HomeworkId);

            modelBuilder
                .Entity<Homework>()
                .Property(p => p.Content)
                .IsUnicode();

            modelBuilder
                .Entity<Homework>()
                .Property(p => p.ContentType)
                .HasConversion<string>();

        }

        private void ConfigureResourceEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Resource>()
                .HasKey(k => k.ResourceId);

            modelBuilder
              .Entity<Resource>()
              .Property(p => p.Name)
              .HasMaxLength(50)
              .IsUnicode();

            modelBuilder
                .Entity<Resource>()
                .Property(p => p.Url)
                .IsUnicode(false);

            modelBuilder
                .Entity<Resource>()
                .Property(p => p.ResourceType)
                .HasConversion<string>();

        }

        private void ConfigureStudentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Student>()
                .HasKey(k => k.StudentId);

            modelBuilder
                .Entity<Student>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode();

            modelBuilder
                .Entity<Student>()
                .Property(p => p.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength(true)
                .IsUnicode(false)
                .IsRequired(false);

            modelBuilder
                .Entity<Student>()
                .Property(p => p.Birthday)
                .IsRequired(false);

            modelBuilder
                .Entity<Student>()
                .HasMany(h => h.HomeworkSubmissions)
                .WithOne(s => s.Student);

        }

        private void ConfigureCourseEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Course>()
                .HasKey(k => k.CourseId);

            modelBuilder
                .Entity<Course>()
                .Property(p => p.Name)
                .HasMaxLength(80)
                .IsUnicode();

            modelBuilder
                .Entity<Course>()
                .Property(p => p.Description)
                .IsUnicode()
                .IsRequired(false);

            modelBuilder
                .Entity<Course>()
                .HasMany(h => h.HomeworkSubmissions)
                .WithOne(c => c.Course);

            modelBuilder
                .Entity<Course>()
                .HasMany(r => r.Resources)
                .WithOne(c => c.Course);

        }
    }
}
