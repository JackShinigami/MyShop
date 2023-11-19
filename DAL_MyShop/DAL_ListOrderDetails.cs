using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        /// <returns>object chứa 2 thuộc tính là ProductId và tổng số lượng</returns>
        public List<dynamic> GetBestSellingProducts(int top)
        {
            List<dynamic> bestSellingProducts = new List<dynamic>();
            var groupProducts = context.OrderDetails.GroupBy(od => od.ProductId)
                .Select(g => new { ProductId = g.Key , Quantity = g.Sum(od => od.Quantity) })
                .OrderByDescending(g => g.Quantity)
                .Take(top)
                .ToList();
            foreach (var item in groupProducts)
            {
                bestSellingProducts.Add(item);
            }

            return bestSellingProducts;
        }
    }

}
