using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Repository
{
    public class RentalDbContext : DbContext
    {
        public DbSet<BoardGames> BoardGame { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public RentalDbContext()
        {
            this.Database.EnsureCreated();
        }

        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WDCO2R_HFT_2023241_Database.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var FilmList = new List<BoardGames>
            {
                new BoardGames {BoardGameId = 1, Title = "Cartographers", Type = "Strategy"},
                new BoardGames {BoardGameId = 2, Title = "Azul", Type = "Logic"},
            };

            Customer c1 = new Customer() { CustomerId = 1, CustomerName = "Kiss Pista", CustomerAge = 24 };
            Customer c2 = new Customer() { CustomerId = 1, CustomerName = "Kiss Jóska", CustomerAge = 24 };
        }
    }
}
