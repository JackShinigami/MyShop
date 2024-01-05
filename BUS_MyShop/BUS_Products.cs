using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL_MyShop;
using DocumentFormat.OpenXml.InkML;
using DTO_MyShop;

namespace BUS_MyShop
{
    public class BUS_Products
    {
        private static BUS_Products? instance;
        private DAL_ListProducts dal = DAL_ListProducts.Instance;
        public enum SortType
        {
            Id,
            ProductName,
            Author,
            PublishYear,
            Publisher,
            CostPrice,
            SellingPrice,
            Quantity
        }
        private BUS_Products()
        {

        }
        public static BUS_Products? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Products();
                return instance;
            }
        }

        public BindingList<Product> GetAllProducts()
        {
            var products = new BindingList<Product>(dal.GetProducts());
            return products;
        }

        public BindingList<Product> GetLowQuantityProduct(int lowQuantity)
        {
            List<Product> products;
            products = dal.GetProducts().Where(p => p.Quantity < lowQuantity).OrderBy(p => p.Quantity).ToList();
            return new BindingList<Product>(products);
        }

        public BindingList<Product> GetProducts(int skip, int take, SortType sortType = SortType.Id, bool ascending = true, string searchKey = "", int beginPrice = 0, int endPrice = int.MaxValue)
        {
            List<Product> products;
            products = dal.GetProducts()
                .Where(p => p.ProductName.ToLower().Contains(searchKey.ToLower()) && p.SellingPrice >= beginPrice && p.SellingPrice <= endPrice).ToList();
            switch (sortType)
            {
                case SortType.Id:
                    if (ascending)
                        products = products.OrderBy(p => p.Id).ToList();
                    else
                        products = products.OrderByDescending(p => p.Id).ToList();
                    break;
                case SortType.ProductName:
                    if (ascending)
                        products = products.OrderBy(p => p.ProductName).ToList();
                    else
                        products = products.OrderByDescending(p => p.ProductName).ToList();
                    break;
                case SortType.Author:
                    if (ascending)
                        products = products.OrderBy(p => p.Author).ToList();
                    else
                        products = products.OrderByDescending(p => p.Author).ToList();
                    break;
                case SortType.PublishYear:
                    if (ascending)
                        products = products.OrderBy(p => p.PublishYear).ToList();
                    else
                        products = products.OrderByDescending(p => p.PublishYear).ToList();
                    break;
                case SortType.Publisher:
                    if (ascending)
                        products = products.OrderBy(p => p.Publisher).ToList();
                    else
                        products = products.OrderByDescending(p => p.Publisher).ToList();
                    break;
                case SortType.CostPrice:
                    if (ascending)
                        products = products.OrderBy(p => p.CostPrice).ToList();
                    else
                        products = products.OrderByDescending(p => p.CostPrice).ToList();
                    break;
                case SortType.SellingPrice:
                    if (ascending)
                        products = products.OrderBy(p => p.SellingPrice).ToList();
                    else
                        products = products.OrderByDescending(p => p.SellingPrice).ToList();
                    break;
                case SortType.Quantity:
                    if (ascending)
                        products = products.OrderBy(p => p.Quantity).ToList();
                    else
                        products = products.OrderByDescending(p => p.Quantity).ToList();
                    break;
                default:
                    break;
            }
            if (skip + take > products.Count)
            {
                take = products.Count - (skip);
            }
            products = products.Skip(skip).Take(take).ToList();
            return new BindingList<Product>(products);
        }

        public BindingList<Product> GetProductsWithCategory(string categoryId, int skip, int take, SortType sortType = SortType.Id, bool ascending = true, string searchKey = "", int beginPrice = 0, int endPrice = int.MaxValue)
        {
            List<Product> products;
            products = dal.GetProducts()
                .Where(p => p.CategoryId.Equals(categoryId)
                && p.ProductName.ToLower().Contains(searchKey.ToLower())
                && p.SellingPrice >= beginPrice && p.SellingPrice <= endPrice).ToList();
            switch (sortType)
            {
                case SortType.Id:
                    if (ascending)
                        products = products.OrderBy(p => p.Id).ToList();
                    else
                        products = products.OrderByDescending(p => p.Id).ToList();
                    break;
                case SortType.ProductName:
                    if (ascending)
                        products = products.OrderBy(p => p.ProductName).ToList();
                    else
                        products = products.OrderByDescending(p => p.ProductName).ToList();
                    break;
                case SortType.Author:
                    if (ascending)
                        products = products.OrderBy(p => p.Author).ToList();
                    else
                        products = products.OrderByDescending(p => p.Author).ToList();
                    break;
                case SortType.PublishYear:
                    if (ascending)
                        products = products.OrderBy(p => p.PublishYear).ToList();
                    else
                        products = products.OrderByDescending(p => p.PublishYear).ToList();
                    break;
                case SortType.Publisher:
                    if (ascending)
                        products = products.OrderBy(p => p.Publisher).ToList();
                    else
                        products = products.OrderByDescending(p => p.Publisher).ToList();
                    break;
                case SortType.CostPrice:
                    if (ascending)
                        products = products.OrderBy(p => p.CostPrice).ToList();
                    else
                        products = products.OrderByDescending(p => p.CostPrice).ToList();
                    break;
                case SortType.SellingPrice:
                    if (ascending)
                        products = products.OrderBy(p => p.SellingPrice).ToList();
                    else
                        products = products.OrderByDescending(p => p.SellingPrice).ToList();
                    break;
                case SortType.Quantity:
                    if (ascending)
                        products = products.OrderBy(p => p.Quantity).ToList();
                    else
                        products = products.OrderByDescending(p => p.Quantity).ToList();
                    break;
                default:
                    break;
            }

            if (skip + take > products.Count)
            {
                take = products.Count - (skip);
            }
            products = products.Skip(skip).Take(take).ToList();
            return new BindingList<Product>(products);
        }

        public Product GetProductById(string id)
        {
            return dal.GetProductById(id);
        }

        public void AddProduct(string id, string ProductName, string Author, int PublishYear, string Publisher,
            int CostPrice, int SellingPrice, string CategoryID, int Quantity, string ImagePath)
        {
            if (CostPrice > SellingPrice)
                throw new Exception("Giá bán phải lớn hơn giá nhập");
            if (Quantity < 0)
                throw new Exception("Số lượng phải lớn hơn 0");
            if (DAL_ListCategories.Instance.GetCategoryById(CategoryID) == null)
                throw new Exception("Loại sản phẩm không tồn tại");

            Product product = new Product()
            {
                Id = id,
                ProductName = ProductName,
                Author = Author,
                PublishYear = PublishYear,
                Publisher = Publisher,
                CostPrice = CostPrice,
                SellingPrice = SellingPrice,
                CategoryId = CategoryID,
                Quantity = Quantity,
                ImagePath = ImagePath
            };

            dal.AddProduct(product);
        }

        public void DeleteProduct(string id)
        {
            dal.DeleteProduct(id);
        }

        public void UpdateProduct(string id, string ProductName, string Author, int PublishYear, string Publisher,
                       int CostPrice, int SellingPrice, string CategoryID, int Quantity, string ImagePath)
        {
            if (CostPrice > SellingPrice)
                throw new Exception("Giá bán phải lớn hơn giá nhập");
            if (Quantity < 0)
                throw new Exception("Số lượng phải lớn hơn 0");
            if (DAL_ListCategories.Instance.GetCategoryById(CategoryID) == null)
                throw new Exception("Loại sản phẩm không tồn tại");

            Product product = new Product()
            {
                Id = id,
                ProductName = ProductName,
                Author = Author,
                PublishYear = PublishYear,
                Publisher = Publisher,
                CostPrice = CostPrice,
                SellingPrice = SellingPrice,
                CategoryId = CategoryID,
                Quantity = Quantity,
                ImagePath = ImagePath
            };

            dal.UpdateProduct(id, product);
        }
    }
}
