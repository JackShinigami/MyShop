using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;

namespace DAL_MyShop
{
    class DAL_ListCustomers
    {
        private static DAL_ListCustomers? instance;
        private BookshopContext context = new BookshopContext();
        public static DAL_ListCustomers? Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_ListCustomers();
                return instance;
            }
        }

        public enum SortType
        {
            Id,
            CustomerName
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers;
            customers = context.Customers.ToList();
            return customers;
        }

        public List<Customer> GetCustomers(int skip, int take, SortType sortType = SortType.Id, bool ascending = true, string searchKey = "")
        {
            List<Customer> customers;
            customers = context.Customers
                .Where(c => c.CustomerName.ToLower().Contains(searchKey.ToLower())).ToList();
            switch (sortType)
            {
                case SortType.Id:
                    if (ascending)
                        customers = customers.OrderBy(c => c.Id).ToList();
                    else
                        customers = customers.OrderByDescending(c => c.Id).ToList();
                    break;
                case SortType.CustomerName:
                    if (ascending)
                        customers = customers.OrderBy(c => c.CustomerName).ToList();
                    else
                        customers = customers.OrderByDescending(c => c.CustomerName).ToList();
                    break;
            }

            customers = customers.Skip(skip).Take(take).ToList();

            return customers;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer.Id == null || customer.Id == "")
            {
                throw new Exception("Customer ID cannot be empty");
            }

            if (context.Customers.Find(customer.Id) != null)
            {
                throw new Exception("Customer ID already exists");
            }
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(string id)
        {
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                throw new Exception("Customer ID does not exist");
            }

            if (context.Orders.Where(o => o.CustomerId == id).Count() > 0)
            {
                throw new Exception("Customer ID is being used by an order");
            }

            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public void UpdateCustomer(string id, Customer customer)
        {
            if (context.Customers.Find(id) == null)
            {
                throw new Exception("Customer ID does not exist");
            }

            if (id != customer.Id)
            {
                throw new Exception("Customer ID cannot be changed");
            }

            Customer c = context.Customers.Find(id);
            c.CustomerName = customer.CustomerName;
            c.Address = customer.Address;
            c.TelephoneNumber = customer.TelephoneNumber;
            
            context.SaveChanges();
        }

        public Customer GetCustomerById(string id)
        {
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                throw new Exception("Customer ID does not exist");
            }

            return customer;
        }
    }
}
