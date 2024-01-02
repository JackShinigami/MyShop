
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
namespace DAL_MyShop
{
    public class DAL_ListProducts
    {
        private static DAL_ListProducts? instance;
        private BookshopContext context = new BookshopContext();
        

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

        public void AddProduct(Product product)
        {
            if (product.Id == null || product.Id == "")
                throw new Exception("Id không thể trống"); 
            if (context.Products.Find(product.Id) != null)
                throw new Exception("Id đã tồn tại");
            
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            Product product = context.Products.Find(id);
            if (product == null)
                throw new Exception("Id không tồn tại");

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void UpdateProduct(string id, Product updatedProduct)
        {
            if(context.Products.Find(id) == null)
                throw new Exception("Id không tồn tại");
            if(id != updatedProduct.Id)
                throw new Exception("Id không thể bị thay đổi");
            
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

        public Product GetProductById(string id)
        {
            Product product = context.Products.Find(id);
            if (product == null)
                throw new Exception("Id không tồn tại");
            return product;
        }
    }
}
