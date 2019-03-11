namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BetEntityConfiguration(modelBuilder);

            ColorEntityConfiguration(modelBuilder);

            CountryEntityConfiguration(modelBuilder);

            GameEntityConfiguration(modelBuilder);

            PlayerEntityConfiguration(modelBuilder);

            PositionEntityConfiguration(modelBuilder);

            TeamEntityConfiguration(modelBuilder);

            TownEntityConfiguration(modelBuilder);

            UserEntityConfiguration(modelBuilder);

            PlayerStatisticEntityConfiguration(modelBuilder);



        }

        private void PlayerStatisticEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PlayerStatistic>()
                .HasKey(k => new { k.PlayerId, k.GameId });

            modelBuilder
                .Entity<PlayerStatistic>()
                .HasOne(p => p.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(fk => fk.PlayerId);

            modelBuilder
               .Entity<PlayerStatistic>()
               .HasOne(g => g.Game)
               .WithMany(p => p.PlayerStatistics)
               .HasForeignKey(fk => fk.GameId);


        }

        private void UserEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<User>()
              .HasKey(k => k.UserId);

            modelBuilder
                .Entity<User>()
                .HasMany(m => m.Bets)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId);
        }

        private void TownEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Town>()
              .HasKey(k => k.TownId);

            modelBuilder
                .Entity<Town>()
                .HasMany(t => t.Teams)
                .WithOne(t => t.Town)
                .HasForeignKey(fk => fk.TownId);
        }

        private void TeamEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Team>(entity =>
              {
                  entity.HasKey(e => e.TeamId);

                  entity.HasOne(c => c.PrimaryKitColor)
                        .WithMany(m => m.PrimaryKitTeams)
                        .HasForeignKey(fk => fk.PrimaryKitColorId)
                        .OnDelete(DeleteBehavior.Restrict);

                  entity.HasOne(c => c.SecondaryKitColor)
                        .WithMany(m => m.SecondaryKitTeams)
                        .HasForeignKey(fk => fk.SecondaryKitColorId)
                        .OnDelete(DeleteBehavior.Restrict);

              });

            modelBuilder
                .Entity<Team>()
                .HasMany(p => p.Players)
                .WithOne(t => t.Team)
                .HasForeignKey(fk => fk.TeamId);

            modelBuilder
                .Entity<Team>()
                .Property(p => p.Name)
                .IsRequired();

        }

        private void PositionEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Position>()
              .HasKey(k => k.PositionId);

            modelBuilder
                .Entity<Position>()
                .HasMany(p => p.Players)
                .WithOne(p => p.Position)
                .HasForeignKey(fk => fk.PositionId);
        }

        private void PlayerEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Player>()
              .HasKey(k => k.PlayerId);
        }

        private void GameEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Game>()
              .HasKey(k => k.GameId);

            modelBuilder
            .Entity<Game>()
            .HasOne<Team>(c => c.HomeTeam)
            .WithMany(m => m.HomeGames)
            .HasForeignKey(fk => fk.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Game>()
               .HasOne<Team>(c => c.AwayTeam)
               .WithMany(m => m.AwayGames)
               .HasForeignKey(fk => fk.AwayTeamId)
               .OnDelete(DeleteBehavior.Restrict);
        }

        private void CountryEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Color>()
                .HasKey(k => k.ColorId);

            modelBuilder
                .Entity<Country>()
                .HasMany(t => t.Towns)
                .WithOne(c => c.Country)
                .HasForeignKey(fk => fk.CountryId);
        }

        private void ColorEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Color>()
                .HasKey(k => k.ColorId);


        }

        private void BetEntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Bet>()
                .HasKey(k => k.BetId);

            modelBuilder
                .Entity<Bet>()
                .HasOne(g => g.Game)
                .WithMany(b => b.Bets)
                .HasForeignKey(fk => fk.GameId);

         
        }
    }
}
