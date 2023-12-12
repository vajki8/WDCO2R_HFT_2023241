using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;
using static WDCO2R_HFT_2023241.Logic.Classes.RentalLogic;

namespace WDCO2R_HFT_2023241.Logic.InterFaces
{
    public interface IRentalLogic
    {
        void Create(Rental rentalId);
        void Delete(int rentalId);
        Rental Read(int rentalId);
        IEnumerable<Rental> ReadAll();
        void Update(Rental rental);
        public IEnumerable<object> OlderCustomer();
        public IEnumerable<object> Withinweek();
        public IEnumerable<object> typeFamily();
    }
}
