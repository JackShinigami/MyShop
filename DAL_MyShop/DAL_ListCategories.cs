using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
namespace DAL_MyShop
{
    class DAL_ListCategories
    {
        private static DAL_ListCategories? instance;
        private BookshopContext context = new BookshopContext();

        public static DAL_ListCategories Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_ListCategories();
                return instance;
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories;
            categories = context.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategories(int skip, int take, string searchKey = "")
        {
            List<Category> categories;
            categories = context.Categories
                .Where(c => c.CategoryName.ToLower().Contains(searchKey.ToLower())).ToList();
            categories = categories.OrderBy(c => c.Id).ToList();
            return categories;
        }

        public void AddCategory(Category category)
        {
            if (category.Id == null || category.Id == "")
            {
                throw new Exception("Category ID cannot be empty");
            }

            if(context.Categories.Find(category.Id) != null)
            {
                throw new Exception("Category ID already exists");
            }
            context.Categories.Add(category);
            context.SaveChanges();
        }
        
        public void DeleteCategory(string id)
        {
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                throw new Exception("Category ID does not exist");
            }

            if (context.Products.Where(p => p.CategoryId == id).Count() > 0)
            {
                throw new Exception("Category ID is being used by a product");
            }

            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void UpdateCategory(string id, Category category)
        {
            if(context.Categories.Find(id) == null)
            {
                throw new Exception("Category ID does not exist");
            }

            if(id != category.Id)
            {
                throw new Exception("Category ID cannot be changed");
            }
            
            context.Categories.Find(id).CategoryName = category.CategoryName;
            context.SaveChanges();
        }
    }
}
