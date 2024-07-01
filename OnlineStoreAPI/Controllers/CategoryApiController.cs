using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineStoreAPI.Controllers
{
    public class CategoryApiController : ApiController
    {
        private readonly CategoryServices _category = new CategoryServices();

        [HttpGet]
        [Route("api/CategoryApi/GetCategory")]
        public List<CategoryModel> GetCategory()
        {
            List<Category> categoryList = _category.GetAllCategories();
            List<CategoryModel> categoryModelList = CategoryConverter.ConvertCategoryListToCategoryModelList(categoryList);
            return categoryModelList;
        }

        [HttpGet]
        [Route("api/CategoryApi/GetSubCategories")]
        public List<SubCategoryModel> GetSubCategories(int subCategoryID)
        {
            List<SubCategory> subCategoryList = _category.GetSubCategory(subCategoryID);
            List<SubCategoryModel> subCategoryModelList = CategoryConverter.ConvertSubCategoryListToSubCategoryModelList(subCategoryList);
            return subCategoryModelList;
        }

        [HttpGet]
        [Route("api/CategoryApi/Category")]
        public List<CategoryModel> Category()
        {
            List<Category> categoryList = _category.GetAllCategories();
            List<CategoryModel> categoryModelList = CategoryConverter.ConvertCategoryListToCategorySelectionModelList(categoryList);
            return categoryModelList;
        }
    }
}