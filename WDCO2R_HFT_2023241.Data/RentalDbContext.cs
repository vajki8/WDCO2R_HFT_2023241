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
            var BoardGameList = new List<BoardGames>
            {
                new BoardGames {BoardGameId = 1, Title = "Cartographers", Type = "Strategy"},
                new BoardGames {BoardGameId = 2, Title = "Azul", Type = "Family"},
                new BoardGames {BoardGameId = 3, Title = "7 Wonders", Type = "Strategy"},
                new BoardGames {BoardGameId = 4, Title = "7 Wonders: Duel", Type = "Strategy"},
                new BoardGames {BoardGameId = 5, Title = "Mars", Type = "Strategy"},
                new BoardGames {BoardGameId = 6, Title = "Citadella", Type = "Family"},
                new BoardGames {BoardGameId = 7, Title = "Imagine", Type = "Party"},
                new BoardGames {BoardGameId = 8, Title = "Jaipur", Type = "Family"},
                new BoardGames {BoardGameId = 9, Title = "Trial by Trolley", Type = "Party"},
                new BoardGames {BoardGameId = 10, Title = "Top10", Type = "Party"},
                new BoardGames {BoardGameId = 11, Title = "Ark Nova", Type = "Strategy"},
                new BoardGames {BoardGameId = 12, Title = "Ticket To Ride", Type = "Family"},
                new BoardGames {BoardGameId = 13, Title = "Wingspan", Type = "Strategy"},
                new BoardGames {BoardGameId = 14, Title = "Cascadia", Type = "Family"},
                new BoardGames {BoardGameId = 15, Title = "Anachrony", Type = "Strategy"},
                new BoardGames {BoardGameId = 16, Title = "Harry Potter: Hogwarts Battle", Type = "Family"},

            };

            Customer user1 = new Customer() { CustomerId = 1, CustomerName = "Kovács János", CustomerAge = 25 };
            Customer user2 = new Customer() { CustomerId = 2, CustomerName = "Nagy Eszter", CustomerAge = 30 };
            Customer user3 = new Customer() { CustomerId = 3, CustomerName = "Kiss Péter", CustomerAge = 28 };
            Customer user4 = new Customer() { CustomerId = 4, CustomerName = "Szabó Anna", CustomerAge = 22 };
            Customer user5 = new Customer() { CustomerId = 5, CustomerName = "Varga Gábor", CustomerAge = 26 };
            Customer user6 = new Customer() { CustomerId = 6, CustomerName = "Tóth Zsuzsa", CustomerAge = 31 };
            Customer user7 = new Customer() { CustomerId = 7, CustomerName = "Molnár András", CustomerAge = 27 };
            Customer user8 = new Customer() { CustomerId = 8, CustomerName = "Horváth Katalin", CustomerAge = 29 };
            Customer user9 = new Customer() { CustomerId = 9, CustomerName = "Papp Márton", CustomerAge = 24 };
            Customer user10 = new Customer() { CustomerId = 10, CustomerName = "Farkas Judit", CustomerAge = 32 };
            Customer user11 = new Customer() { CustomerId = 11, CustomerName = "Balogh Bence", CustomerAge = 23 };
            Customer user12 = new Customer() { CustomerId = 12, CustomerName = "Szilágyi Krisztina", CustomerAge = 33 };
            Customer user13 = new Customer() { CustomerId = 13, CustomerName = "Gál Norbert", CustomerAge = 26 };
            Customer user14 = new Customer() { CustomerId = 14, CustomerName = "Csernák Enikő", CustomerAge = 28 };
            Customer user15 = new Customer() { CustomerId = 15, CustomerName = "Pintér Dávid", CustomerAge = 30 };
            Customer user16 = new Customer() { CustomerId = 16, CustomerName = "Fehér Viktória", CustomerAge = 27 };

            
        }
    }
}
