using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Repository
{
    public class RentalRepository
    {
        RentalDbContext context;
        public RentalRepository(RentalDbContext context)
        {
            this.context = context;
        }

        public void Create(Rental rental)
        {
            context.Rentals.Add(rental);
            context.SaveChanges();
        }

        public void Delete(int rentalId)
        {
            context.Remove(Read(rentalId));
            context.SaveChanges();
        }

        public Rental Read(int rentalId)
        {
            return context.Rentals.FirstOrDefault(t => t.RentId == rentalId);
        }

        public IQueryable<Rental> ReadAll()
        {
            return context.Rentals;
        }

        public void Update(Rental rental)
        {
            var updated = Read(rental.RentId);
            updated.RentId=rental.RentId;
            updated.CustomerId=rental.CustomerId;
            updated.BoardGameId=rental.BoardGameId;
            context.SaveChanges();
        }
    }
}
