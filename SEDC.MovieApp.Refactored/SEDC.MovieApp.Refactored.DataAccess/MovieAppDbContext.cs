using Microsoft.EntityFrameworkCore;
using SEDC.MovieApp.Refactored.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.DataAccess
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Genre)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .HasData(
                    new Movie { Id = 1, Title = "Avengers", Description = "Best fantasy movie", Year = 2017, Genre = Domain.Enums.Genre.Fantasy },
                    new Movie { Id = 2, Title = "Top gun", Description = "Best action movie", Year = 2019, Genre = Domain.Enums.Genre.Action }
                );

            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.FavoriteGenre)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasData(
                        new User { Id = 1, UserName = "acomilovski", FirstName = "Aleksandar", LastName = "Milovski", Password = "aco123", FavoriteGenre = Domain.Enums.Genre.Action }
                );
        }
    }
}
