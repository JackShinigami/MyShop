using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO_MyShop;
namespace DAL_MyShop
{
    class DAL_ListOrderDetails
    {
        private static DAL_ListOrderDetails? instance;
        private BookshopContext context = new BookshopContext();
        public static DAL_ListOrderDetails? Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_ListOrderDetails();
                return instance;
            }
        }

        public List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderDetails;
            orderDetails = context.OrderDetails.ToList();
            return orderDetails;
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(string orderId)
        {
            List<OrderDetail> orderDetails;
            orderDetails = context.OrderDetails.Where(od => od.OrderId == orderId).ToList();
            return orderDetails;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail.OrderId == null || orderDetail.OrderId == "")
            {
                throw new Exception("Order ID cannot be empty");
            }

            if (orderDetail.ProductId == null || orderDetail.ProductId == "")
            {
                throw new Exception("Product ID cannot be empty");
            }

            if (context.OrderDetails.Find(orderDetail.OrderId, orderDetail.ProductId) != null)
            {
                throw new Exception("Order detail already exists");
            }

            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
        }

        public void DeleteOrderDetail(string orderId, string productId)
        {
            OrderDetail orderDetail = context.OrderDetails.Find(orderId, productId);
            if (orderDetail == null)
            {
                throw new Exception("Order detail does not exist");
            }
            context.OrderDetails.Remove(orderDetail);
            context.SaveChanges();
        }

        public void UpdateOrderDetail(string orderId, string productId, OrderDetail orderDetail)
        {
            if(context.OrderDetails.Find(orderId, productId) == null)
            {
                throw new Exception("Order detail does not exist");
            }

            if(orderId != orderDetail.OrderId)
            {
                throw new Exception("Order ID cannot be changed");
            }
            
            if(productId != orderDetail.ProductId)
            {
                throw new Exception("Product ID cannot be changed");
            }

            OrderDetail od = context.OrderDetails.Find(orderId, productId);
            od.Quantity = orderDetail.Quantity;
            context.SaveChanges();
        }    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns>object chứa 3 thuộc tính là ProductId, tổng số lượng, ProductName</returns>
        public List<dynamic> GetBestSellingProducts(int top)
        {
            List<dynamic> bestSellingProducts = new List<dynamic>();
            var groupProducts = context.OrderDetails
                .GroupBy(od => od.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(od => od.Quantity) })
                .OrderByDescending(g => g.Quantity)
                .Take(top)
                .ToList();

            var newGroupProducts = groupProducts.Join(context.Products, g => g.ProductId, p => p.Id, (g, p) => (g, p))
                .Select(t => new { t.g.ProductId, t.g.Quantity, t.p.ProductName }).ToList();
               

            foreach (var item in newGroupProducts)
            {
                bestSellingProducts.Add(item);
            }

            return bestSellingProducts;
        }

        public dynamic GetRevenueAndProfit(DateTime beginDate, DateTime endDate)
        {
            var temp = from od in context.OrderDetails
                       from o in context.Orders
                       from p in context.Products
                       where od.OrderId == o.Id && od.ProductId == p.Id && o.OrderDate >= beginDate && o.OrderDate <= endDate
                       select new { Revenue = od.Quantity * p.SellingPrice, Profit = od.Quantity * (p.SellingPrice - p.CostPrice) };
            var res = (from r in temp
                       group r by 1 into g
                       select new { Revenue = g.Sum(r => r.Revenue), Profit = g.Sum(r => r.Profit) }).ToList();

            if (res.Count == 0)
                return new { Revenue = 0, Profit = 0 };

            return res[0];
        }

        public List<dynamic> GetSalesOfProducts(DateTime beginDate, DateTime endDate)
        {
            var res = new List<dynamic>();
            
            var temp = from od in context.OrderDetails
                       from o in context.Orders
                       from p in context.Products
                       where od.OrderId == o.Id && od.ProductId == p.Id && o.OrderDate >= beginDate && o.OrderDate <= endDate
                       select new { ProductId = p.Id, ProductName = p.ProductName, od.Quantity};
            var group = (from t in temp
                        group t by t.ProductId into g
                        select new { ProductId = g.Key, ProductName = g.First().ProductName, Quantity = g.Sum(t => t.Quantity) }).ToList();
            foreach (var item in group)
            {
                res.Add(item);
            }
            return res;
        }
    }

}
