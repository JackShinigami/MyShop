using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
using DAL_MyShop;
using System.ComponentModel;
using System.Security.Cryptography;
using DocumentFormat.OpenXml.InkML;

namespace BUS_MyShop
{
    public class BUS_Orders
    {
        private static BUS_Orders? instance;
        private DAL_ListOrders dal = DAL_ListOrders.Instance;
        public enum SortType
        {
            Id,
            CustomerId,
            OrderDate,
        }
        private BUS_Orders()
        {

        }
        public static BUS_Orders? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Orders();
                return instance;
            }
        }

        public BindingList<Order> GetAllOrders()
        {
            var orders = dal.GetOrders();
            return new BindingList<Order>(orders);
        }

        public BindingList<Order> GetOrders(int skip, int take, SortType sortType = SortType.Id, bool ascending = true, DateTime beginDate = default, DateTime endDate = default)
        {
            if (beginDate == default)
            {
                beginDate = DateTime.MinValue;
            }
            if (endDate == default)
            {
                endDate = DateTime.MaxValue;
            }

            List<Order> orders;
            orders = dal.GetOrders()
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

            return new BindingList<Order>(orders);
        }

        public void AddOrder(string Id, string CustomerId, DateTime OrderDate)
        {
            if(DAL_ListCustomers.Instance.GetCustomerById(CustomerId) == null)
            {
                throw new Exception("Id khách hàng không tồn tại");
            }

            Order order = new Order()
            {
                Id = Id,
                CustomerId = CustomerId,
                OrderDate = OrderDate,
            };

            dal.AddOrder(order);
        }

        public void DeleteOrder(string id)
        {
            BUS_OrderDetails.Instance.DeleteOrderDetailsByOrderId(id);
            dal.DeleteOrder(id);
        }

        public void UpdateOrder(string id, string CustomerId, DateTime OrderDate)
        {
            if (DAL_ListCustomers.Instance.GetCustomerById(CustomerId) == null)
            {
                throw new Exception("Id khách hàng không tồn tại");
            }

            Order order = new Order()
            {
                Id = id,
                CustomerId = CustomerId,
                OrderDate = OrderDate,
            };

            dal.UpdateOrder(id, order);
        }
    }

}
