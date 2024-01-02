using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
namespace DAL_MyShop
{
    public class DAL_ListOrders
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

        public List<Order> GetOrders()
        {
            List<Order> orders;
            orders = context.Orders.ToList();
            return orders;
        }

        public void AddOrder(Order order)
        {
            if (order.Id == null || order.Id == "")
            {
                throw new Exception("Id đơn hàng không được trống");
            }

            if (context.Orders.Find(order.Id) != null)
            {
                throw new Exception("Id đơn hàng đã tồn tại");
            }
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void DeleteOrder(string id)
        {
            Order order = context.Orders.Find(id);
            if (order == null)
            {
                throw new Exception("Id đơn hàng không tồn tại");
            }

            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public void UpdateOrder(string id, Order order)
        {
            if (context.Orders.Find(id) == null)
            {
                throw new Exception("Id đơn hàng không tại");
            }

            if (id != order.Id)
            {
                throw new Exception("Id đơn hàng không được thay đổi");
            }

            Order o = context.Orders.Find(id);
            o.CustomerId = order.CustomerId;
            o.OrderDate = order.OrderDate;
            
            context.SaveChanges();
        }

        public Order GetOrderById(string id)
        {
            Order order = context.Orders.Find(id);
            if (order == null)
            {
                throw new Exception("Id đơn hàng không tồn tại");
            }
            return order;
        }
    }
}
