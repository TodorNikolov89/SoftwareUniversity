using System;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagraph.Data
{
    public class InstagraphContext : DbContext
    {
        public InstagraphContext() { }

        public InstagraphContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<UserFollower> UsersFollowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserConfiguration(modelBuilder);
            UserFollowerConfiguration(modelBuilder);
            PictureConfiguration(modelBuilder);
            PostConfiguration(modelBuilder);
            CommentConfiguration(modelBuilder);
        }

        private void UserFollowerConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFollower>()
        .HasKey(x => new { x.UserId, x.FollowerId });


            modelBuilder
                .Entity<UserFollower>()
                .HasOne(x => x.User)
                .WithMany(u => u.UsersFollowing)
                .HasForeignKey(fk => fk.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<UserFollower>()
               .HasOne(x => x.Follower)
               .WithMany(u => u.Followers)
               .HasForeignKey(fk => fk.UserId)
               .OnDelete(DeleteBehavior.Restrict);


        }

        private void CommentConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
         .HasOne(b => b.User)
         .WithMany(a => a.Comments)
         .OnDelete(DeleteBehavior.Restrict);
        }

        private void PostConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Post>()
                .HasMany(c => c.Comments)
                .WithOne(p => p.Post);
        }

        private void PictureConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Picture>()
                .HasMany(u => u.Users)
                .WithOne(p => p.ProfilePicture);

            modelBuilder
                .Entity<Picture>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Picture);
        }

        private void UserConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(p => p.Username)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<User>()
               .HasMany(p => p.Comments)
               .WithOne(u => u.User);



        }
    }
}
