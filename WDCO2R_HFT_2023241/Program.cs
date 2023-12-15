using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            Console.Clear();
            Console.WriteLine("Enter the parameters:");
            if (entity.ToLower() == "boardgame")
            {
                Console.WriteLine("BoardGameId: ");
                int boradgameid = int.Parse(Console.ReadLine());
                Console.WriteLine("Title: ");
                string boardgametitle = Console.ReadLine();
                Console.WriteLine("Type: ");
                string boardgametype = Console.ReadLine();

                rest.Post(new BoardGames() { BoardGameId = boradgameid, Title = boardgametitle, Type = boardgametype }, "boardgame");
            }
            else if (entity.ToLower() == "customer")
            {
                Console.WriteLine("CustomerId: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Age: ");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Name: ");
                string name = Console.ReadLine();

                rest.Post(new Customer() { CustomerId = id, CustomerAge = age, CustomerName = name }, "customer");
            }
            else if (entity.ToLower() == "rental")
            {
                Console.WriteLine("RentId: ");
                int rentid = int.Parse(Console.ReadLine());
                Console.WriteLine("BoardGameId: ");
                int boardgameid = int.Parse(Console.ReadLine());
                Console.WriteLine("CustomerId: ");
                int customerid = int.Parse(Console.ReadLine());
                Console.WriteLine("Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Price: ");
                double price = double.Parse(Console.ReadLine());
                Console.WriteLine("TimeLeft: ");
                double time = double.Parse(Console.ReadLine());

                rest.Post(new Rental() { RentId = rentid, BoardGameId = boardgameid, CustomerId = customerid, Price = price, TimeLeft = time, Name = name}, "rental");
            }
            Console.WriteLine("\n" + entity + " added to the db");
            Console.Write("Press any button to continue");
            Console.ReadKey();
        }
        static void Delete(string entity)
        {
            if (entity.ToLower() == "boardgame")
            {
                Console.Clear();
                Console.Write("Enter the id of the boardgame that you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "boardgame");
            }
            else if (entity.ToLower() == "customer")
            {
                Console.Clear();
                Console.Write("Enter the id of the customer that you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "customer");
            }
            else if (entity.ToLower() == "rental")
            {
                Console.Clear();
                Console.Write("Enter the id of the rental that you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "rental");
            }
            Console.WriteLine($"\n{entity.ToUpper()} deleted");
            Console.Write("\nPress any button to continue");
            Console.ReadKey();
        }
        #region Reads
        static void ReadBoardGame<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("Id of the boardgame you want to get: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(rest.Get<BoardGames>(id, "boardgames").ToString());
            Console.Write("\nPress any button to continue");
            Console.ReadKey();
        }
        static void ReadCustomer<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("Id of the customer you want to get: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(rest.Get<Customer>(id, "customer").ToString());
            Console.Write("\nPress any button to continue");
            Console.ReadKey();
        }
        static void ReadRental<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("Id of the rental you want to get: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(rest.Get<Rental>(id, "rental").ToString());
            Console.Write("\nPress any button to continue");
            Console.ReadKey();
        }
        static void ReadAll<T>(List<T> list, string entity)
        {
            Console.Clear();
            Console.WriteLine($"List of {entity.ToUpper()} in the db: \n");
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.Write("\nPress any button to continue");
            Console.ReadKey();
        }
        #endregion
        static void Update(string entity)
        {
            Console.WriteLine("Enter the requested param(s): ");
            if (entity.ToLower() == "boardgame")
            {
                Console.Write("Enter the id of the game that you want to update: ");
                int id = int.Parse(Console.ReadLine());
                BoardGames one = rest.Get<BoardGames>(id, "boardgame");
                Console.Write($"New name [old: {one.Title}]: ");
                string name = Console.ReadLine();
                one.Title = name;
                rest.Put(one, "boardgame");
            }
            else if (entity.ToLower() == "customer")
            {
                Console.Write("Enter the id of the customer that you want to update: ");
                int id = int.Parse(Console.ReadLine());
                Customer one = rest.Get<Customer>(id, "customer");
                Console.Write($"New name [old: {one.CustomerName}]: ");
                string name = Console.ReadLine();
                one.CustomerName = name;
                rest.Put(one, "customer");
            }
            else if (entity.ToLower() == "rental")
            {
                Console.Write("Enter the id of the rental that you want to update: ");
                int id = int.Parse(Console.ReadLine());
                Rental one = rest.Get<Rental>(id, "rental");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "rental");
            }
            Console.WriteLine("\n" + entity.ToUpper() + " updated");
            Console.Write("\nPress any button to continue");
            Console.ReadKey();
        }

        static void OlderCustomer(string entity)
        {
            Console.WriteLine("The senior members: ");
            var customers = rest.Get<object>(entity);
            foreach (var item in customers)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void Family(string entity)
        {
            Console.WriteLine("The family type games: ");
            var family = rest.Get<object>(entity);
            foreach (var item in family)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void currentCustomer(string entity)
        {
            Console.WriteLine("The selected customer: ");
            var customer = rest.Get<object>(entity);
            foreach (var item in customer)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void HighestRent(string entity)
        {
            Console.WriteLine("The longest rent time: ");
            var rent = rest.Get<object>(entity);
            foreach (var item in rent)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void Freegames(string entity)
        {
            Console.WriteLine("The free rents: ");
            var free = rest.Get<object>(entity);
            foreach (var item in free)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void Withinweek(string entity)
        {
            Console.WriteLine("Customers with low rent time left: ");
            var free = rest.Get<object>(entity);
            foreach (var item in free)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:35357/","swagger");

            ConsoleMenu menu = new ConsoleMenu();

            menu.Add("BoardGame", 
                () => new ConsoleMenu()
                .Add("Create", () => Create("boardgame"))
                .Add("Delete", () => Delete("boardgame"))
                .Add("Lists of all items", () => ReadAll<BoardGames>(rest.Get<BoardGames>("boardgame"), "game"))
                .Add("Read by ID", () => ReadBoardGame("boardgame"))
                .Add("Update", () => Update("boardgame"))
                .Add("Back to menu", ConsoleMenu.Close)
                .Show()
                );

            menu.Add("Customer", 
                () => new ConsoleMenu()
                .Add("Create", () => Create("customer"))
                .Add("Delete", () => Delete("customer"))
                .Add("Lists of all items", () => ReadAll<Customer>(rest.Get<Customer>("customer"), "customer"))
                .Add("Read by ID", () => ReadCustomer("customer"))
                .Add("Update", () => Update("customer"))
                .Add("Back to menu", ConsoleMenu.Close)
                .Show()
                );

            menu.Add("Rental", 
                () => new ConsoleMenu()
                .Add("Create", () => Create("rental"))
                .Add("Delete", () => Delete("rental"))
                .Add("Lists of all items", () => ReadAll<Rental>(rest.Get<Rental>("rental"), "rental"))
                .Add("Read by ID", () => ReadRental("rental"))
                .Add("Update", () => Update("rental"))
                .Add("Back to menu", ConsoleMenu.Close)
                .Show()
                );

            menu.Add("Non CRUD methods",
                () => new ConsoleMenu()
                .Add("Senior members", () => OlderCustomer("stat/OlderCustomer"))
                .Add("HighestRent", () => HighestRent("stat/HighestTime"))
                .Add("FreeRents", () => Freegames("stat/FreePrice"))
                .Add("Current Customers", () => currentCustomer("stat/currentCustomer"))
                .Add("Family games", () => Family("stat/typeFamily"))
                .Add("One week left", () => Withinweek("stat/withinWeek"))
                .Add("Back to menu", ConsoleMenu.Close)
                .Show()
                );

            menu.Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
