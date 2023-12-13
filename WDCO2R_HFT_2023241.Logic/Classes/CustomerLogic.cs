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
    public class CustomerLogic : ICustomerLogic
    {
        ICustomerRepository Customerrepos;

        public CustomerLogic(ICustomerRepository cusrepos)
        {
            Customerrepos = cusrepos;
        }
        public void Create(Customer customer)
        {
            if (customer.CustomerId < 1)
            {
                throw new NullReferenceException("ID can't be less than 1");
            }
            else if (customer.CustomerName == null || customer.CustomerName == "")
            {
                throw new NullReferenceException("Name can't be empty");
            }
            else if (customer.CustomerAge < 18)
            {
                throw new NullReferenceException("Must be older than 18");
            }
            else if (customer.CustomerName.Length > 50)
            {
                throw new ArgumentException("The name is too long");
            }
            else
            {
                Customerrepos.Create(customer);
            }
        }

        public void Delete(int customerId)
        {
            if (customerId < 0)
            {
                throw new ArgumentOutOfRangeException("ID does not exist, can't be deleted");
            }
            else
            {
                Customerrepos.Delete(customerId);
            }
        }

        public Customer Read(int customerId)
        {
            if (customerId < 0)
            {
                throw new ArgumentOutOfRangeException("ID does not exist, can't be deleted");
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
            else if (customer.CustomerId < 1)
            {
                throw new NullReferenceException("ID can't be less than 1");
            }
            else if (customer.CustomerName == null || customer.CustomerName == "")
            {
                throw new NullReferenceException("Name can't be empty");
            }
            else if (customer.CustomerAge < 18)
            {
                throw new NullReferenceException("Must be older than 18");
            }
            else if (customer.CustomerName.Length > 50)
            {
                throw new ArgumentException("The name is too long");
            }
            else
            {
                Customerrepos.Update(customer);
            }

        }
    }
}
