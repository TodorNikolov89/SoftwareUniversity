namespace VaporStore.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using VaporStore.Data.Models;

    public class VaporStoreDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameTag> GameTags { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }


        public VaporStoreDbContext()
        {
        }

        public VaporStoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            UsersConfiguration(model);
            CardsConfiguration(model);
            CengresConfiguration(model);
            DevelopersConfiguration(model);
            TagsConfiguration(model);
            GameTagsConfiguration(model);
            GamesConfiguration(model);

        }

        private void GamesConfiguration(ModelBuilder model)
        {
            model
                .Entity<Game>()
                .HasMany(p => p.Purchases)
                .WithOne(g => g.Game)
                .HasForeignKey(f => f.GameId);

            model
                .Entity<Game>()
                .HasMany(t => t.GameTags)
                .WithOne(g => g.Game)
                .HasForeignKey(f => f.GameId);
        }

        private void GameTagsConfiguration(ModelBuilder model)
        {
            model
                .Entity<GameTag>()
                .HasKey(pk => new { pk.GameId, pk.TagId });

            model
                .Entity<GameTag>()
                .HasOne(g => g.Game)
                .WithMany(gt => gt.GameTags)
                .HasForeignKey(fk => fk.GameId);

            model
                .Entity<GameTag>()
                .HasOne(t => t.Tag)
                .WithMany(gt => gt.GameTags)
                .HasForeignKey(fk => fk.TagId);

        }

        private void TagsConfiguration(ModelBuilder model)
        {
            model
                 .Entity<Tag>()
                 .HasMany(gt => gt.GameTags)
                 .WithOne(t => t.Tag)
                 .HasForeignKey(f => f.TagId);
        }

        private void DevelopersConfiguration(ModelBuilder model)
        {
            model
                 .Entity<Developer>()
                 .HasMany(g => g.Games)
                 .WithOne(d => d.Developer)
                 .HasForeignKey(f => f.DeveloperId);
        }

        private void CengresConfiguration(ModelBuilder model)
        {
            model
                .Entity<Genre>()
                .HasMany(g => g.Games)
                .WithOne(ge => ge.Genre)
                .HasForeignKey(f => f.GenreId);
        }

        private void CardsConfiguration(ModelBuilder model)
        {
            model
                .Entity<Card>()
                .HasMany(p => p.Purchases)
                .WithOne(c => c.Card)
                .HasForeignKey(f => f.CardId);
        }

        private void UsersConfiguration(ModelBuilder model)
        {
            model
                .Entity<User>()
                .HasMany(c => c.Cards)
                .WithOne(u => u.User)
                .HasForeignKey(f => f.UserId);
        }
    }
}