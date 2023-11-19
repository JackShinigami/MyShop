using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
namespace DAL_MyShop
{
    class DAL_ListOrders
    {
        private static DAL_ListOrders? instance;
        private BookshopContext context = new BookshopContext();
        public static DAL_ListOrders? Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_ListOrders();
                return instance;
            }
        }

        public enum SortType
        {
            Id,
            CustomerId,
            OrderDate,
        }

        public List<Order> GetOrders()
        {
            List<Order> orders;
            orders = context.Orders.ToList();
            return orders;
        }

        public List<Order> GetOrders(int skip, int take, SortType sortType = SortType.Id, bool ascending = true, DateTime beginDate = default, DateTime endDate = default)
        {
            if(beginDate == default)
            {
                beginDate = DateTime.MinValue;
            }
            if(endDate == default)
            {
                endDate = DateTime.MaxValue;
            }

            List<Order> orders;
            orders = context.Orders
                .Where(o => o.OrderDate >= beginDate && o.OrderDate <= endDate)
                .ToList();

            switch (sortType)
            {
                case SortType.Id:
                    if (ascending)
                    {
                        orders = orders.OrderBy(o => o.Id).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(o => o.Id).ToList();
                    }
                    break;
                case SortType.CustomerId:
                    if (ascending)
                    {
                        orders = orders.OrderBy(o => o.CustomerId).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(o => o.CustomerId).ToList();
                    }
                    break;
                case SortType.OrderDate:
                    if (ascending)
                    {
                        orders = orders.OrderBy(o => o.OrderDate).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(o => o.OrderDate).ToList();
                    }
                    break;
                default:
                    break;
            }

            return orders;
        }

        public void AddOrder(Order order)
        {
            if (order.Id == null || order.Id == "")
            {
                throw new Exception("Order ID cannot be empty");
            }

            if (context.Orders.Find(order.Id) != null)
            {
                throw new Exception("Order ID already exists");
            }
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void DeleteOrder(string id)
        {
            Order order = context.Orders.Find(id);
            if (order == null)
            {
                throw new Exception("Order ID does not exist");
            }

            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public void UpdateOrder(string id, Order order)
        {
            if (context.Orders.Find(id) == null)
            {
                throw new Exception("Order ID does not exist");
            }

            if (id != order.Id)
            {
                throw new Exception("Order ID cannot be changed");
            }

            Order o = context.Orders.Find(id);
            o.CustomerId = order.CustomerId;
            o.OrderDate = order.OrderDate;
            
            context.SaveChanges();
        }
    }
}
