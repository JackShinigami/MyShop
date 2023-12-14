using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
using DAL_MyShop;
using DocumentFormat.OpenXml.InkML;
namespace BUS_MyShop
{
    public class BUS_OrderDetails
    {
        private static BUS_OrderDetails? instance;
        private DAL_MyShop.DAL_ListOrderDetails dal = DAL_MyShop.DAL_ListOrderDetails.Instance;
        private BUS_OrderDetails()
        {

        }
        public static BUS_OrderDetails? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_OrderDetails();
                return instance;
            }
        }


        public BindingList<OrderDetail> GetAllOrderDetails()
        {
            var orderDetails = dal.GetOrderDetails();  
            return new BindingList<OrderDetail>(orderDetails);
        }

        public BindingList<OrderDetail> GetOrderDetailsByOrderId(string orderId)
        {
            var orderDetails = dal.GetOrderDetailsByOrderId(orderId);
            return new BindingList<OrderDetail>(orderDetails);
        }

        public void AddOrderDetail(string OrderId, string ProductId, int Quantity)
        {
            if(OrderId == null || OrderId == "")
            {
                throw new Exception("Id đơn hàng không được trống");
            }
            if(ProductId == null || ProductId == "")
            {
                throw new Exception("Id sản phẩm không được trống");
            }
            if(DAL_ListProducts.Instance.GetProductById(ProductId) == null)
            {
                throw new Exception("Sản phẩm không tồn tại");
            }
            if(DAL_ListOrders.Instance.GetOrderById(OrderId) == null)
            {
                throw new Exception("Đơn hàng không tồn tại");
            }
            if(Quantity <= 0)
            {
                throw new Exception("Số lượng phải lớn hơn 0");
            }
            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = OrderId,
                ProductId = ProductId,
                Quantity = Quantity
            };

            dal.AddOrderDetail(orderDetail);
        }

        public void DeleteOrderDetail(string orderId, string productId)
        {
            dal.DeleteOrderDetail(orderId, productId);
        }

        public void DeleteOrderDetailsByOrderId(string orderId)
        {
            List<OrderDetail> orderDetails = dal.GetOrderDetailsByOrderId(orderId);
            foreach(OrderDetail orderDetail in orderDetails)
            {
                dal.DeleteOrderDetail(orderDetail.OrderId, orderDetail.ProductId);
            }
        }

        public void UpdateOrderDetail(string orderId, string productId, int Quantity)
        {
            if (dal.GetOrderDetailById(orderId, productId) == null)
            {
                throw new Exception("Id thông tin đơn hàng không tồn tại");
            }

            if (Quantity <= 0)
            {
                throw new Exception("Số lượng phải lớn hơn 0");
            }

            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = Quantity
            };

            dal.UpdateOrderDetail(orderId, productId, orderDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns>object chứa 3 thuộc tính là ProductId, tổng số lượng, ProductName</returns>
        public BindingList<dynamic> GetBestSellingProducts(int top)
        {
            List<dynamic> bestSellingProducts = new List<dynamic>();
            var groupProducts = dal.GetOrderDetails()
                .GroupBy(od => od.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(od => od.Quantity) })
                .OrderByDescending(g => g.Quantity)
                .Take(top)
            .ToList();

            var newGroupProducts = groupProducts.Join(DAL_ListProducts.Instance.GetProducts(), g => g.ProductId, p => p.Id, (g, p) => (g, p))
                .Select(t => new { t.g.ProductId, t.g.Quantity, t.p.ProductName }).ToList();


            foreach (var item in newGroupProducts)
            {
                bestSellingProducts.Add(item);
            }

            return new BindingList<dynamic>(bestSellingProducts);
        }

        public dynamic GetRevenueAndProfit(DateTime beginDate, DateTime endDate)
        {
            var temp = from od in dal.GetOrderDetails()
                       from o in DAL_ListOrders.Instance.GetOrders()
                       from p in DAL_ListProducts.Instance.GetProducts()
                       where od.OrderId == o.Id && od.ProductId == p.Id && o.OrderDate >= beginDate && o.OrderDate <= endDate
                       select new { Revenue = od.Quantity * p.SellingPrice, Profit = od.Quantity * (p.SellingPrice - p.CostPrice) };
            var res = (from r in temp
                       group r by 1 into g
                       select new { Revenue = g.Sum(r => r.Revenue), Profit = g.Sum(r => r.Profit) }).ToList();

            if (res.Count == 0)
                return new { Revenue = 0, Profit = 0 };

            return res[0];
        }

        public BindingList<dynamic> GetSalesOfProducts(DateTime beginDate, DateTime endDate)
        {
            var res = new List<dynamic>();

            var temp = from od in dal.GetOrderDetails()
                       from o in DAL_ListOrders.Instance.GetOrders()
                       from p in DAL_ListProducts.Instance.GetProducts()
                       where od.OrderId == o.Id && od.ProductId == p.Id && o.OrderDate >= beginDate && o.OrderDate <= endDate
                       select new { ProductId = p.Id, ProductName = p.ProductName, od.Quantity };
            var group = (from t in temp
                         group t by t.ProductId into g
                         select new { ProductId = g.Key, ProductName = g.First().ProductName, Quantity = g.Sum(t => t.Quantity) }).ToList();
            foreach (var item in group)
            {
                res.Add(item);
            }
            return new BindingList<dynamic>(res);
        }
    }
}
