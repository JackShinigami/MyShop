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

            Product product = DAL_ListProducts.Instance.GetProductById(ProductId);
            if(product.Quantity < Quantity)
            {
                throw new Exception("Số lượng sản phẩm trong kho không đủ");
            }
            
            Product newProduct = new Product()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Author = product.Author,
                PublishYear = product.PublishYear,
                Publisher = product.Publisher,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                Quantity = product.Quantity - Quantity
            };

            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = OrderId,
                ProductId = ProductId,
                Quantity = Quantity
            };
            dal.AddOrderDetail(orderDetail);
            DAL_ListProducts.Instance.UpdateProduct(ProductId, newProduct);
        }

        public void DeleteOrderDetail(string orderId, string productId)
        {
            
            Product product = DAL_ListProducts.Instance.GetProductById(productId);
            Product newProduct = new Product()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Author = product.Author,
                PublishYear = product.PublishYear,
                Publisher = product.Publisher,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                Quantity = product.Quantity + dal.GetOrderDetailById(orderId, productId).Quantity
            };

            
            dal.DeleteOrderDetail(orderId, productId);
            DAL_ListProducts.Instance.UpdateProduct(productId, newProduct);
        }

        public void DeleteOrderDetailsByOrderId(string orderId)
        {
            List<OrderDetail> orderDetails = dal.GetOrderDetailsByOrderId(orderId);
            foreach(OrderDetail orderDetail in orderDetails)
            {
                DeleteOrderDetail(orderId, orderDetail.ProductId);
            }
        }

        public void UpdateOrderDetail(string orderId, string productId, int Quantity)
        {
            OrderDetail oldOrderDetail = dal.GetOrderDetailById(orderId, productId);
            if (oldOrderDetail == null)
            {
                throw new Exception("Id thông tin đơn hàng không tồn tại");
            }

            if (Quantity <= 0)
            {
                throw new Exception("Số lượng phải lớn hơn 0");
            }

            Product product = DAL_ListProducts.Instance.GetProductById(productId);
            if(product.Quantity + oldOrderDetail.Quantity < Quantity)
            {
                throw new Exception("Số lượng sản phẩm trong kho không đủ");
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

        public Tuple<List<DateTime>,List<int>, List<int>> GetRevenueAndProfitByDay(DateTime beginDate, DateTime endDate)
        {
            var Dates = new List<DateTime>();
            var Revenues = new List<int>();
            var Profits = new List<int>();

            var temp = from od in dal.GetOrderDetails()
                       from o in DAL_ListOrders.Instance.GetOrders()
                       from p in DAL_ListProducts.Instance.GetProducts()
                       where od.OrderId == o.Id && od.ProductId == p.Id && o.OrderDate >= beginDate && o.OrderDate <= endDate
                       orderby o.OrderDate
                       select new { o.OrderDate, Revenue = od.Quantity * p.SellingPrice, Profit = od.Quantity * (p.SellingPrice - p.CostPrice) };
            var group = (from t in temp
                         group t by t.OrderDate into g
                         select new { OrderDate = g.Key, Revenue = g.Sum(t => t.Revenue), Profit = g.Sum(t => t.Profit) }).ToList();
            foreach (var item in group)
            {
                Dates.Add((DateTime)item.OrderDate);
                Revenues.Add((int)item.Revenue);
                Profits.Add((int)item.Profit);
            }
            return new Tuple<List<DateTime>, List<int>, List<int>>(Dates, Revenues, Profits);
        }

        public List<Tuple<string, int>> GetSalesOfProducts(DateTime beginDate, DateTime endDate)
        {
            var res = new List<Tuple<string, int>>();

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
                res.Add(new Tuple<string, int>(item.ProductName, (int)item.Quantity));
            }
            return res;
        }

        public Tuple<int, double> TotalPriceAndDiscount(List<OrderDetail> orderDetails)
        {
            int sum = 0;
            double discount = 0;
            foreach (var item in orderDetails)
            {
                Product product = DAL_ListProducts.Instance.GetProductById(item.ProductId);
                sum += (int)item.Quantity * (int)product.SellingPrice;
            }

            if(sum >= 1000000)
            {
                discount = 0.1;
            }
            else if(sum >= 500000)
            {
                discount = 0.05;
            }

            sum = (int)(sum * (1 - discount));
            return new Tuple<int, double>(sum, discount);
        }
    }
}
