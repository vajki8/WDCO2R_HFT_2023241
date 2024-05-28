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
        public DbSet<BoardGame> BoardGame { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public RentalDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseInMemoryDatabase("BoardGameDb")
                .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>(entity =>
            {
                entity
                .HasMany(b => b.Rental)
                .WithOne(r => r.BoardGame)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                .HasMany(c => c.Rental)
                .WithOne(r => r.Customer)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity
                .HasOne(r => r.BoardGame)
                .WithMany(b => b.Rental)
                .HasForeignKey(r => r.BoardGameId)
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .HasOne(r => r.Customer)
                .WithMany(b => b.Rental)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });


            BoardGame bgame1 = new BoardGame { BoardGameId = 1, Title = "Cartographers", Type = "Strategy" };
            BoardGame bgame2 = new BoardGame { BoardGameId = 2, Title = "Azul", Type = "Family" };
            BoardGame bgame3 = new BoardGame { BoardGameId = 3, Title = "7 Wonders", Type = "Strategy" };
            BoardGame bgame4 = new BoardGame { BoardGameId = 4, Title = "7 Wonders: Duel", Type = "Strategy" };
            BoardGame bgame5 = new BoardGame { BoardGameId = 5, Title = "Mars", Type = "Strategy" };
            BoardGame bgame6 = new BoardGame { BoardGameId = 6, Title = "Citadella", Type = "Family" };
            BoardGame bgame7 = new BoardGame { BoardGameId = 7, Title = "Imagine", Type = "Party" };
            BoardGame bgame8 = new BoardGame { BoardGameId = 8, Title = "Jaipur", Type = "Family" };
            BoardGame bgame9 = new BoardGame { BoardGameId = 9, Title = "Trial by Trolley", Type = "Party" };
            BoardGame bgame10 = new BoardGame { BoardGameId = 10, Title = "Top10", Type = "Party" };
            BoardGame bgame11 = new BoardGame { BoardGameId = 11, Title = "Ark Nova", Type = "Strategy" };
            BoardGame bgame12 = new BoardGame { BoardGameId = 12, Title = "Ticket To Ride", Type = "Family" };
            BoardGame bgame13 = new BoardGame { BoardGameId = 13, Title = "Wingspan", Type = "Strategy" };
            BoardGame bgame14 = new BoardGame { BoardGameId = 14, Title = "Cascadia", Type = "Family" };
            BoardGame bgame15 = new BoardGame { BoardGameId = 15, Title = "Anachrony", Type = "Strategy" };
            BoardGame bgame16 = new BoardGame { BoardGameId = 16, Title = "Harry Potter: Hogwarts Battle", Type = "Family" };

            

            Customer user1 = new Customer() { CustomerId = 1, CustomerName = "Kovács János", CustomerAge = 25 };
            Customer user2 = new Customer() { CustomerId = 2, CustomerName = "Nagy Eszter", CustomerAge = 30 };
            Customer user3 = new Customer() { CustomerId = 3, CustomerName = "Kiss Péter", CustomerAge = 28 };
            Customer user4 = new Customer() { CustomerId = 4, CustomerName = "Szabó Anna", CustomerAge = 22 };
            Customer user5 = new Customer() { CustomerId = 5, CustomerName = "Varga Gábor", CustomerAge = 26 };
            Customer user6 = new Customer() { CustomerId = 6, CustomerName = "Tóth Zsuzsa", CustomerAge = 31 };
            Customer user7 = new Customer() { CustomerId = 7, CustomerName = "Molnár András", CustomerAge = 66 };
            Customer user8 = new Customer() { CustomerId = 8, CustomerName = "Horváth Katalin", CustomerAge = 29 };
            Customer user9 = new Customer() { CustomerId = 9, CustomerName = "Papp Márton", CustomerAge = 24 };
            Customer user10 = new Customer() { CustomerId = 10, CustomerName = "Farkas Judit", CustomerAge = 32 };
            Customer user11 = new Customer() { CustomerId = 11, CustomerName = "Balogh Bence", CustomerAge = 23 };
            Customer user12 = new Customer() { CustomerId = 12, CustomerName = "Szilágyi Krisztina", CustomerAge = 33 };
            Customer user13 = new Customer() { CustomerId = 13, CustomerName = "Gál Norbert", CustomerAge = 26 };
            Customer user14 = new Customer() { CustomerId = 14, CustomerName = "Csernák Enikő", CustomerAge = 28 };
            Customer user15 = new Customer() { CustomerId = 15, CustomerName = "Pintér Dávid", CustomerAge = 30 };
            Customer user16 = new Customer() { CustomerId = 16, CustomerName = "Fehér Viktória", CustomerAge = 27 };

            Rental rent1 = new Rental() { RentId = 1,   Name="Berles1",     BoardGameId = bgame15.BoardGameId,    CustomerId = user4.CustomerId,   Price = 2500,  TimeLeft = 12};
            Rental rent2 = new Rental() { RentId = 2,   Name="Berles2",     BoardGameId = bgame4.BoardGameId,     CustomerId = user12.CustomerId,  Price = 16000, TimeLeft = 30};
            Rental rent3 = new Rental() { RentId = 3,   Name="Berles3",     BoardGameId = bgame13.BoardGameId,    CustomerId = user16.CustomerId,  Price = 9000,  TimeLeft = 18};
            Rental rent4 = new Rental() { RentId = 4,   Name="Berles4",     BoardGameId = bgame7.BoardGameId,     CustomerId = user4.CustomerId,   Price = 42000, TimeLeft = 360};
            Rental rent5 = new Rental() { RentId = 5,   Name="Berles5",     BoardGameId = bgame9.BoardGameId,     CustomerId = user3.CustomerId,   Price = 6000,  TimeLeft = 16};
            Rental rent6 = new Rental() { RentId = 6,   Name="Berles6",     BoardGameId = bgame1.BoardGameId,     CustomerId = user13.CustomerId,  Price = 23000, TimeLeft = 26};
            Rental rent7 = new Rental() { RentId = 7,   Name="Berles7",     BoardGameId = bgame6.BoardGameId,     CustomerId = user7.CustomerId,   Price = 500,   TimeLeft = 2};
            Rental rent8 = new Rental() { RentId = 8,   Name="Berles8",     BoardGameId = bgame10.BoardGameId,    CustomerId = user1.CustomerId,   Price = 0,     TimeLeft = 1};
            Rental rent9 = new Rental() { RentId = 9,   Name="Berles9",     BoardGameId = bgame4.BoardGameId,     CustomerId = user14.CustomerId,  Price = 6300,  TimeLeft = 8};
            Rental rent10 = new Rental() { RentId = 10, Name="Berles10",     BoardGameId = bgame3.BoardGameId,     CustomerId = user5.CustomerId,   Price = 4700,  TimeLeft = 14};
            Rental rent11 = new Rental() { RentId = 11, Name="Berles11",     BoardGameId = bgame15.BoardGameId,    CustomerId = user2.CustomerId,   Price = 70000, TimeLeft = 700};
            Rental rent12 = new Rental() { RentId = 12, Name="Berles12",     BoardGameId = bgame11.BoardGameId,    CustomerId = user10.CustomerId,  Price = 1200,  TimeLeft = 4};

            modelBuilder.Entity<BoardGame>().HasData(bgame1,bgame2,bgame3,bgame4,bgame5,bgame6,bgame7,bgame8,bgame9,bgame10,bgame11,bgame12,bgame13,bgame14,bgame15,bgame16);
            modelBuilder.Entity<Customer>().HasData(user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, user11, user12, user13, user14, user15, user16);
            modelBuilder.Entity<Rental>().HasData(rent1, rent2, rent3, rent4, rent5, rent6, rent7, rent8, rent9, rent10, rent11, rent12);
        }
    }
}
