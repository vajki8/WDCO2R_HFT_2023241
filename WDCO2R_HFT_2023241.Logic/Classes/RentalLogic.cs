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

        public RentalLogic(IRentalRepository rentalrepos, IBoardGameRepository boardrep, ICustomerRepository cusrep)
        {
            rentRep = rentalrepos;
            cusRep = cusrep;
            boardRep = boardrep;

        }

        public void Create(Rental rental)
        {
            if (rental.Price > 80001)
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
        public IEnumerable<object> OlderCustomer()
        {
            var older = from x in rentRep.ReadAll()
                         where x.Customer.CustomerAge > 50
                         select new
                         {
                             _CustomerName = x.Customer.CustomerName,
                             _CustomerAge = x.Customer.CustomerAge,
                         };
            return older;
        }

        public IEnumerable<object> typeFamily()
        {
            var type = from x in rentRep.ReadAll()
                       where x.BoardGame.Type.ToUpper().Contains("FAMILY")
                       select new
                       {
                           _BoardGameName = x.BoardGame.Title,
                           _BoardGameType = x.BoardGame.Type
                       };
            return type;
        }
        public IEnumerable<KeyValuePair<string, string>> currentCustomers()
        {
            var current = from x in rentRep.ReadAll()
                          select new KeyValuePair<string, string>
                          (x.Customer.CustomerName, x.BoardGame.Title);
            return current;
        }

        public IEnumerable<KeyValuePair<string, double>> HighestTime()
        {
            var high = from x in rentRep.ReadAll()
                       where x.TimeLeft.Equals(rentRep.ReadAll().Max(x => x.TimeLeft))
                       select new KeyValuePair<string, double>
                       (x.Customer.CustomerName, x.TimeLeft);
            return high;
        }

        public IEnumerable<KeyValuePair<string, double>> FreePrice()
        {
            var price = from x in rentRep.ReadAll()
                        where x.Price == 0
                       select new KeyValuePair<string, double>
                       (x.Customer.CustomerName, x.Price);
            return price;
        }


        public IEnumerable<object> Withinweek()
        {
            var week = from x in rentRep.ReadAll()
                        where x.TimeLeft < 8
                        select new
                        {
                            _CustomerName = x.Customer.CustomerName,
                            _TimeLeft = x.TimeLeft
                        };
            return week;
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
                return HashCode.Combine(this.CustomerName, this.CustomerAge);
            }
        }
    }
}
