
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
namespace DAL_MyShop
{
    class DAL_ListProducts
    {
        private static DAL_ListProducts? instance;
        private BookshopContext context = new BookshopContext();
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

        public static DAL_ListProducts Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_ListProducts();
                return instance;
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products;
            products = context.Products.ToList();
            return products;
        }

        public List<Product> GetLowQuantityProduct(int lowQuantity)
        {
            List<Product> products;
            products = context.Products.Where(p => p.Quantity < lowQuantity).OrderBy(p => p.Quantity).ToList();
            return products;
        }

        public List<Product> GetProducts(int skip , int take,  SortType sortType = SortType.Id, string searchKey="", int beginPrice=0, int endPrice= int.MaxValue)
        {
            List<Product> products;
            products = context.Products
                .Where(p => p.ProductName.ToLower().Contains(searchKey.ToLower()) && p.SellingPrice >= beginPrice && p.SellingPrice <= endPrice).ToList();
            switch (sortType)
            {
                case SortType.Id:
                    products = products.OrderBy(p => p.Id).ToList();
                    break;
                case SortType.ProductName:
                    products = products.OrderBy(p => p.ProductName).ToList();
                    break;
                case SortType.Author:
                    products = products.OrderBy(p => p.Author).ToList();
                    break;
                case SortType.PublishYear:
                    products = products.OrderBy(p => p.PublishYear).ToList();
                    break;
                case SortType.Publisher:
                    products = products.OrderBy(p => p.Publisher).ToList(); 
                    break;
                case SortType.CostPrice:
                    products = products.OrderBy(p => p.CostPrice).ToList();
                    break;
                case SortType.SellingPrice:
                    products = products.OrderBy(p => p.SellingPrice).ToList();
                    break;
                case SortType.Quantity:
                    products = products.OrderBy(p => p.Quantity).ToList();
                    break;
                default:
                    break;
            }   
            products = products.Skip(skip).Take(take).ToList();
            return products;
        }
        
        public List<Product> GetProductsWithCategory(string categoryId, int skip, int take, SortType sortType = SortType.Id, string searchKey = "", int beginPrice = 0, int endPrice = int.MaxValue)
        {
            List<Product> products;
            products = context.Products
                .Where(p => p.CategoryId.Equals(categoryId) 
                && p.ProductName.ToLower().Contains(searchKey.ToLower())
                && p.SellingPrice >= beginPrice && p.SellingPrice <= endPrice).ToList();
            switch (sortType)
            {
                case SortType.Id:
                    products = products.OrderBy(p => p.Id).ToList();
                    break;
                case SortType.ProductName:
                    products = products.OrderBy(p => p.ProductName).ToList();
                    break;
                case SortType.Author:
                    products = products.OrderBy(p => p.Author).ToList();
                    break;
                case SortType.PublishYear:
                    products = products.OrderBy(p => p.PublishYear).ToList();
                    break;
                case SortType.Publisher:
                    products = products.OrderBy(p => p.Publisher).ToList();
                    break;
                case SortType.CostPrice:
                    products = products.OrderBy(p => p.CostPrice).ToList();
                    break;
                case SortType.SellingPrice:
                    products = products.OrderBy(p => p.SellingPrice).ToList();
                    break;
                case SortType.Quantity:
                    products = products.OrderBy(p => p.Quantity).ToList();
                    break;
                default:
                    break;
            }
            products = products.Skip(skip).Take(take).ToList();
            return products;
        }

        public void AddProduct(Product product)
        {
            if (product.Id == null || product.Id == "")
                throw new Exception("Id is null"); 
            if (context.Products.Find(product.Id) != null)
                throw new Exception("Id is existed");
            
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            Product product = context.Products.Find(id);
            if (product == null)
                throw new Exception("Id is not existed");

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void UpdateProduct(string id, Product updatedProduct)
        {
            if(context.Products.Find(id) == null)
                throw new Exception("Id is not existed");
            if(id != updatedProduct.Id)
                throw new Exception("Id cannot be change");
            
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            product!.ProductName = updatedProduct.ProductName;
            product!.Author = updatedProduct.Author;
            product!.PublishYear = updatedProduct.PublishYear;
            product!.Publisher = updatedProduct.Publisher;
            product!.CostPrice = updatedProduct.CostPrice;
            product!.SellingPrice = updatedProduct.SellingPrice;
            product!.Quantity = updatedProduct.Quantity;
            product!.CategoryId = updatedProduct.CategoryId;
            product!.ImagePath = updatedProduct.ImagePath;

            context.SaveChanges();
        }
    }
}
