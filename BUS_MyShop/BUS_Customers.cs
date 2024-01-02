using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
using DAL_MyShop;
using System.Windows.Data;
using System.ComponentModel;
using DocumentFormat.OpenXml.InkML;
namespace BUS_MyShop
{
    public class BUS_Customers
    {
        private static BUS_Customers? instance;
        private DAL_ListCustomers dal = DAL_ListCustomers.Instance;
        public enum SortType
        {
            Id,
            CustomerName
        }
        private BUS_Customers()
        {

        }
        public static BUS_Customers? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Customers();
                return instance;
            }
        }

        public BindingList<Customer> GetAllCustomers()
        {
            var customers = new BindingList<Customer>(dal.GetCustomers());
            return customers;
        }

        public BindingList<Customer> GetCustomers(int skip, int take, SortType sortType = SortType.Id, bool ascending = true, string searchKey = "")
        {
            List<Customer> customers;
            customers = dal.GetCustomers()
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

            return new BindingList<Customer>(customers);
        }

        public void AddCustomer(string Id, string CustomerName, string Address, string PhoneNumber)
        {
            Customer customer = new Customer()
            {
                Id = Id,
                CustomerName = CustomerName,
                Address = Address,
                TelephoneNumber = PhoneNumber
            };

            dal.AddCustomer(customer);
        }

        public void DeleteCustomer(string id)
        {
            dal.DeleteCustomer(id);
        }

        public void UpdateCustomer(string id, string updatedCustomerName, string updatedAddress, string updatedTelephoneNumber)
        {
            Customer updatedCustomer = new Customer()
            {
                Id = id,
                CustomerName = updatedCustomerName,
                Address = updatedAddress,
                TelephoneNumber = updatedTelephoneNumber
            };

            dal.UpdateCustomer(id, updatedCustomer);
        }
    }
}
