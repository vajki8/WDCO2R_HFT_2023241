using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Repository
{
    interface IRentalRepository
    {
        void Create(Rental rentalId);
        void Delete(int rentalId);
        Rental Read(int rentalId);
        IQueryable<Rental> ReadAll();
        void Update(Rental rental);
    }
}
