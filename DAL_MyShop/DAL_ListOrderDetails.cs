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
    public class DAL_ListOrderDetails
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
                throw new Exception("Id đơn hàng không được để trống");
            }

            if (orderDetail.ProductId == null || orderDetail.ProductId == "")
            {
                throw new Exception("Id sản phẩm không được để trống");
            }

            if (context.OrderDetails.Find(orderDetail.OrderId, orderDetail.ProductId) != null)
            {
                throw new Exception("Thông tin đơn hàng đã tồn tại");
            }

            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
        }

        public void DeleteOrderDetail(string orderId, string productId)
        {
            OrderDetail orderDetail = context.OrderDetails.Find(orderId, productId);
            if (orderDetail == null)
            {
                throw new Exception("Thông tin đơn hàng đã tồn tại");
            }
            context.OrderDetails.Remove(orderDetail);
            context.SaveChanges();
        }

        public void UpdateOrderDetail(string orderId, string productId, OrderDetail orderDetail)
        {
            if(context.OrderDetails.Find(orderId, productId) == null)
            {
                throw new Exception("Thông tin đơn hàng không tồn tại");
            }

            if(orderId != orderDetail.OrderId)
            {
                throw new Exception("Id đơn hàng không được thay đổi");
            }
            
            if(productId != orderDetail.ProductId)
            {
                throw new Exception("Id sản phẩm không được thay đổi");
            }

            OrderDetail od = context.OrderDetails.Find(orderId, productId);
            od.Quantity = orderDetail.Quantity;
            context.SaveChanges();
        }    

        public OrderDetail GetOrderDetailById(string orderId, string productId)
        {
            OrderDetail orderDetail = context.OrderDetails.Find(orderId, productId);
            if (orderDetail == null)
            {
                throw new Exception("Thông tin đơn hàng không tồn tại");
            }
            return orderDetail;
        }   
    }

}
