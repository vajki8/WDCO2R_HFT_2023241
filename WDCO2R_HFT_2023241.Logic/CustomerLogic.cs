using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;
using WDCO2R_HFT_2023241.Repository;

namespace WDCO2R_HFT_2023241.Logic
{
    public class CustomerLogic : ICustomerLogic
    {
        ICustomerRepository Customerrepos;

        public CustomerLogic(ICustomerRepository cusrepos)
        {
            Customerrepos = cusrepos;
        }
        public void Create(Customer customer)
        {
            if (customer == null)
            {
                throw new NullReferenceException();
            }
            else if (customer.CustomerAge <= 18)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (customer.CustomerName == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                Customerrepos.Create(customer);
            }
        }

        public void Delete(int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Customerrepos.Delete(customerId);
        }

        public Customer Read(int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException();

            }
            else
            {
                return Customerrepos.Read(customerId);
            }

        }

        public IEnumerable<Customer> ReadAll()
        {
            return Customerrepos.ReadAll();
        }

        public void Update(Customer customer)
        {

            if (customer == null)
            {
                throw new NullReferenceException();
            }
            else if (customer.CustomerAge < 12)
            {
                throw new ArgumentOutOfRangeException("Must be older than 18");
            }
            else if (customer.CustomerName == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                Customerrepos.Update(customer);
            }
        }
    }
}
