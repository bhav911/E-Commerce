using OnlineStoreModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Interface
{
    public interface ICategoryInterface
    {
        List<Category> GetAllCategories();
        List<SubCategory> GetSubCategory(int categoryID);
    }
}
