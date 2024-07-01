using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class CategoryConverter
    {
        public static List<CategoryModel> ConvertCategoryListToCategorySelectionModelList(List<Category> categoryList)
        {
            List<CategoryModel> categoryModelList = new List<CategoryModel>();

            foreach (Category category in categoryList)
            {
                CategoryModel categoryModel = new CategoryModel()
                {
                    CategoryID = category.categoryID,
                    Name = category.name,
                    SubCategoryList = new List<SubCategoryModel>()
                };

                List<SubCategory> subCategoryList = category.SubCategory.ToList();
                foreach (SubCategory subCategory in subCategoryList)
                {
                    SubCategoryModel subCategoryModel = new SubCategoryModel()
                    {
                        SubCategoryID = subCategory.subCategoryID,
                        Name = subCategory.name
                    };
                    categoryModel.SubCategoryList.Add(subCategoryModel);
                }

                categoryModelList.Add(categoryModel);
            }

            return categoryModelList;
        }

        public static List<SubCategoryModel> ConvertSubCategoryListToSubCategoryModelList(List<SubCategory> subCategoryList)
        {
            List<SubCategoryModel> subCategoryModelList = new List<SubCategoryModel>();
            foreach (SubCategory subCategory in subCategoryList)
            {
                SubCategoryModel subCategoryModel = new SubCategoryModel()
                {
                    CategoryID = subCategory.categoryID,
                    Name = subCategory.name,
                    SubCategoryID = subCategory.subCategoryID,
                    Description = subCategory.description,
                    ProductCount = subCategory.productCount
                };

                subCategoryModelList.Add(subCategoryModel);
            }

            return subCategoryModelList;
        }

        public static List<CategoryModel> ConvertCategoryListToCategoryModelList(List<Category> categoryList)
        {
            List<CategoryModel> categoryModelList = new List<CategoryModel>();
            foreach (Category category in categoryList)
            {
                CategoryModel categoryModel = new CategoryModel()
                {
                    CategoryID = category.categoryID,
                    Name = category.name,
                    Description = category.description
                };

                categoryModelList.Add(categoryModel);
            }

            return categoryModelList;
        }

        public static List<CategoryModel> ConvertCategoryListToCategoryModelListForHomePage(List<Category> categoryList)
        {
            List<CategoryModel> categoryModelList = new List<CategoryModel>();
            foreach (Category category in categoryList)
            {
                CategoryModel categoryModel = new CategoryModel()
                {
                    CategoryID = category.categoryID,
                    Name = category.name,
                    Description = category.description,
                };

                List<SubCategoryModel> subCategoryModelList = new List<SubCategoryModel>();
                foreach (SubCategory subCategory in category.SubCategory)
                {
                    SubCategoryModel subCategoryModel = new SubCategoryModel()
                    {
                        SubCategoryID = subCategory.subCategoryID,
                        Name = subCategory.name,
                        ImagePath = subCategory.SubCategoryImage.FirstOrDefault().fileName
                    };
                    subCategoryModelList.Add(subCategoryModel);
                }

                categoryModel.SubCategoryList = subCategoryModelList;
                categoryModelList.Add(categoryModel);
            }

            return categoryModelList;
        }
    }
}
