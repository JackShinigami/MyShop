using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_MyShop;
using DAL_MyShop;
using System.ComponentModel;
using DocumentFormat.OpenXml.InkML;

namespace BUS_MyShop
{
    public class BUS_Categories
    {
        private static BUS_Categories? instance;
        private DAL_ListCategories dal = DAL_ListCategories.Instance;
        private BUS_Categories()
        {

        }
        public static BUS_Categories? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Categories();
                return instance;
            }
        }

        public BindingList<Category> GetAllCategories()
        {
            var categories = new BindingList<Category>(dal.GetCategories());
            return categories;
        }

        public BindingList<Category> GetCategories(int skip, int take, string searchKey = "")
        {
            List<Category> categories;

            categories = dal.GetCategories()
                .Where(c => c.CategoryName.ToLower().Contains(searchKey.ToLower())).ToList();
            categories = categories.OrderBy(c => c.Id).ToList();

            return new BindingList<Category>(categories);
        }
        
        public void AddCategory(string Id, string CategoryName)
        {
            Category category = new Category()
            {
                Id = Id,
                CategoryName = CategoryName
            };

            dal.AddCategory(category);
        }

        public void DeleteCategory(string id)
        {
            dal.DeleteCategory(id);
        }

        public void UpdateCategory(string id, string updatedCategoryName)
        {
            Category updatedCategory = new Category()
            {
                Id = id,
                CategoryName = updatedCategoryName
            };

            dal.UpdateCategory(id, updatedCategory);
        }
    }
}
