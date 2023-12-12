using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Logic.InterFaces;
using WDCO2R_HFT_2023241.Models;
using WDCO2R_HFT_2023241.Repository;

namespace WDCO2R_HFT_2023241.Logic.Classes
{
    public class RentalLogic : IRentalLogic
    {
        IRentalRepository rentRep;
        ICustomerRepository cusRep;
        IBoardGameRepository boardRep;
        public RentalLogic(IRentalRepository rentalrepos)
        {
            rentRep = rentalrepos;
        }

        public RentalLogic(IRentalRepository rentalrepos, IBoardGameRepository boardrep, ICustomerRepository cusrep)
        {
            rentRep = rentalrepos;
            cusRep = cusrep;
            boardRep = boardrep;

        }

        public void Create(Rental rental)
        {
            if (rental.RentId < 1 || rental.BoardGameId < 1 || rental.CustomerId < 1)
            {
                throw new NullReferenceException("ID can't be less than 1");
            }
            else if (rental.Price > 80001)
            {
                throw new ArgumentException("Rent price can't be higher than 80k Ft");
            }
            else if (rental.TimeLeft > 721)
            {
                throw new ArgumentException("Rent can't be older than 2 year");
            }
            else
            {
                rentRep.Create(rental);
            }
        }

        public void Delete(int rentalId)
        {
            if (rentalId < 1)
            {
                throw new NullReferenceException("ID can't be less than 1");
            }
            else
            {
                rentRep.Delete(rentalId);
            }

        }

        public Rental Read(int rentalId)
        {
            if (rentalId < 0)
            {
                throw new ArgumentOutOfRangeException("ID can't be less than 1");
            }
            else
            {
                return rentRep.Read(rentalId);
            }
        }

        public IEnumerable<Rental> ReadAll()
        {
            return rentRep.ReadAll();
        }

        public void Update(Rental rental)
        {
            if (rental.RentId < 1 || rental.BoardGameId < 1 || rental.CustomerId < 1)
            {
                throw new NullReferenceException("ID can't be less than 1");
            }
            else if (rental.Price > 80001)
            {
                throw new ArgumentException("Rent price can't be higher than 80k Ft");
            }
            else if (rental.TimeLeft > 721)
            {
                throw new ArgumentException("Rent can't be older than 2 year");
            } 
            else
            {
                rentRep.Update(rental);
            }
        }

        //Non CRUD

        public IEnumerable<object> HighestValueGame()
        {
            var highest = from x in rentRep.ReadAll()
                          where x.Price.Equals(rentRep.ReadAll().Max(t => t.Price))
                          select new
                          {
                              _BoardGame = x.BoardGame.Title,
                              _Customer = x.Customer.CustomerName,
                              _Price = x.Price,
                          };
            return highest;
        }
        public IEnumerable<RentInfoTime> MaxTimeRent()
        {
            var maxtime = from x in this.rentRep.ReadAll()
                          where x.TimeLeft.Equals(rentRep.ReadAll().Max(t => t.TimeLeft))
                          select new RentInfoTime()
                          {
                              BoardGameName = x.BoardGame.Title,
                              CustomerName = x.Customer.CustomerName,
                              TimeLeft = x.TimeLeft,
                          };
            return maxtime;
        }
        public IEnumerable<RentInfoTime> MinTimeRent()
        {
            var mintime = from x in this.rentRep.ReadAll()
                          where x.TimeLeft.Equals(rentRep.ReadAll().Min(t => t.TimeLeft))
                          select new RentInfoTime()
                          {
                              BoardGameName = x.BoardGame.Title,
                              CustomerName = x.Customer.CustomerName,
                              TimeLeft = x.TimeLeft,
                          };
            return mintime;
        }
        public IEnumerable<RentInfoCustomer> OldestCustomer()
        {
            var oldest = from x in this.cusRep.ReadAll()
                          where x.CustomerAge.Equals(cusRep.ReadAll().Max(t => t.CustomerAge))
                          select new RentInfoCustomer()
                          {
                              CustomerName = x.CustomerName,
                              CustomerAge = x.CustomerAge,
                          };
            return oldest;
        }
        public IEnumerable<RentInfoCustomer> YoungestCustomer()
        {
            var youngest = from x in this.cusRep.ReadAll()
                         where x.CustomerAge.Equals(cusRep.ReadAll().Min(t => t.CustomerAge))
                         select new RentInfoCustomer()
                         {
                             CustomerName = x.CustomerName,
                             CustomerAge = x.CustomerAge,
                         };
            return youngest;
        }

        public class RentInfoTime
        {
            public string BoardGameName{ get; set; }
            public string CustomerName{ get; set; }
            public double TimeLeft { get; set; }

            public override bool Equals(object obj)
            {
                RentInfoTime r = obj as RentInfoTime;
                if(r == null)
                {
                    return false;
                }
                else
                {
                    return this.CustomerName== r.CustomerName && this.BoardGameName == r.BoardGameName && this.TimeLeft == r.TimeLeft;
                }
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }

        public class RentInfoCustomer
        {
            public string CustomerName{ get; set; }
            public double CustomerAge { get; set; }

            public override bool Equals(object obj)
            {
                RentInfoCustomer r = obj as RentInfoCustomer;
                if (r == null)
                {
                    return false;
                }
                else
                {
                    return this.CustomerName == r.CustomerName && this.CustomerAge == r.CustomerAge;
                }
            }
            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }
    }
}
