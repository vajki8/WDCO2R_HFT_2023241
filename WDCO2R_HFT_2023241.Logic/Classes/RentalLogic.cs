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
        IRentalRepository Rentalrepos;

        public RentalLogic(IRentalRepository librep, IBoardGameRepository bookrep, ICustomerRepository cusrep)
        {
            Rentalrepos = librep;
        }

        public void Create(Rental rental)
        {
            if (rental.BoardGameId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (rental.CustomerId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Rentalrepos.Create(rental);
            }

        }

        public void Delete(int rentalId)
        {
            if (rentalId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Rentalrepos.Delete(rentalId);
            }

        }

        public Rental Read(int rentalId)
        {
            if (rentalId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return Rentalrepos.Read(rentalId);
            }
        }

        public IEnumerable<Rental> ReadAll()
        {
            return Rentalrepos.ReadAll();
        }

        public void Update(Rental rental)
        {
            if (rental == null)
            {
                throw new NullReferenceException();
            }
            else if (rental.BoardGameId <= 0 || rental.CustomerId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Rentalrepos.Update(rental);
            }
        }
    }
}
