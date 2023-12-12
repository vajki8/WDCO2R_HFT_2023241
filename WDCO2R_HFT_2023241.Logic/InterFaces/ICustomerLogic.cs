using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Logic.InterFaces
{
    public interface ICustomerLogic
    {
        void Create(Customer customer);
        void Delete(int customerId);
        Customer Read(int customerId);
        IEnumerable<Customer> ReadAll();
        void Update(Customer customer);
    }
}
