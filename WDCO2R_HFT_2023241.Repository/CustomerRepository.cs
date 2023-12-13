using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        RentalDbContext context;
        public CustomerRepository(RentalDbContext context)
        {
            this.context = context;
        }
        public void Create(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void Delete(int customerId)
        {
            context.Remove(Read(customerId));
            context.SaveChanges();
        }

        public Customer Read(int customerId)
        {
            return context.Customers.FirstOrDefault(x => x.CustomerId == customerId);
        }

        public IQueryable<Customer> ReadAll()
        {
            return context.Customers;
        }

        public void Update(Customer customer)
        {
            var updated = Read(customer.CustomerId);
            updated.CustomerAge = customer.CustomerAge;
            updated.CustomerId = customer.CustomerId;
            updated.CustomerName = customer.CustomerName;
            context.SaveChanges();
        }
    }
}
