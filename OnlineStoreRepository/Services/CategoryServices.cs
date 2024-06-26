using OnlineStoreModel.Context;
using OnlineStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class CategoryServices : ICategoryInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public List<Category> GetAllCategories()
        {
            List<Category> categoryList = db.Category.ToList();
            return categoryList;
        }
        public List<SubCategory> GetSubCategory(int categoryID)
        {
            List<SubCategory> subCategorieList = db.SubCategory.Where(q => q.categoryID == categoryID).ToList();
            return subCategorieList;
        }
    }
}
