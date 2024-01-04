using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
namespace DAL_MyShop
{
    public class DAL_ListCategories
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


        public void AddCategory(Category category) 
        {
            if (category.Id == null || category.Id == "")
            {
                throw new Exception("Id không thể trống");
            }

            if(context.Categories.Find(category.Id) != null)
            {
                throw new Exception("Id đã tồn tại");
            }
            context.Categories.Add(category);
            context.SaveChanges();
        }
        
        public void DeleteCategory(string id)
        {
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                throw new Exception("Id không tồn tại");
            }

            if (context.Products.Where(p => p.CategoryId == id).Count() > 0)
            {
                throw new Exception("Vui lòng xoá các sản phẩm của loại sản phẩm này trước");
            }

            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void UpdateCategory(string id, Category category)
        {
            if(context.Categories.Find(id) == null)
            {
                throw new Exception("Id không tồn tại");
            }

            if(id != category.Id)
            {
                throw new Exception("Id không thể thay đổi");
            }
            
            context.Categories.Find(id).CategoryName = category.CategoryName;
            context.SaveChanges();
        }

        public Category GetCategoryById(string id)
        {
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                throw new Exception("Id không tồn tại");
            }
            return category;
        }

        public int GetCount()
        {
            return context.Categories.Count();
        }
    }
}
