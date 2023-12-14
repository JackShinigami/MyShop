using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;

namespace DAL_MyShop
{
    public class DAL_ListCustomers
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

       

        public List<Customer> GetCustomers()
        {
            List<Customer> customers;
            customers = context.Customers.ToList();
            return customers;
        }

        
        public void AddCustomer(Customer customer)
        {
            if (customer.Id == null || customer.Id == "")
            {
                throw new Exception("Id không thể trống");
            }

            if (context.Customers.Find(customer.Id) != null)
            {
                throw new Exception("Id đã tồn tại");
            }
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(string id)
        {
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                throw new Exception("Id không tồn tại");
            }

            if (context.Orders.Where(o => o.CustomerId == id).Count() > 0)
            {
                throw new Exception("Vui lòng xoá các đơn hàng của khách hàng này trước");
            }

            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        public void UpdateCustomer(string id, Customer customer)
        {
            if (context.Customers.Find(id) == null)
            {
                throw new Exception("Id không tồn tại");
            }

            if (id != customer.Id)
            {
                throw new Exception("Id không thể thay đổi");
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
                throw new Exception("Id không tồn tại");
            }

            return customer;
        }
    }
}
