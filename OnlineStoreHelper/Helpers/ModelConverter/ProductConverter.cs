using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class ProductConverter
    {
        public static Products ConvertProductModelToProduct(ProductModel productModel, int shopID)
        {
            Products product = new Products()
            {
                ProductName = productModel.ProductName,
                ProductDescription = productModel.ProductDescription,
                ProductPrice = productModel.ProductPrice,
                OwnerID = shopID,
                Availability = productModel.Availability,
                isDeleted = false,
                subCategoryID = productModel.SubCategoryID
            };
            return product;
        }

        public static ProductModel ConvertProductToProductModel(Products product)
        {
            ProductModel productModel = new ProductModel()
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = (decimal)product.ProductPrice,
                Availability = (bool)product.Availability,
                ProductID = product.ProductID,
                OwnerID = (int)product.OwnerID,
                ImageID = product.ProductImages.FirstOrDefault().ImageID,
                SubCategory = product.SubCategory.name,
                Category = product.SubCategory.Category.name,
                SubCategoryID = product.subCategoryID,
                CategoryID = product.SubCategory.categoryID,
               InStock = product.InStock
            };
            string paths = product.ProductImages.FirstOrDefault().uniqueImageName;
            if (paths != null && paths.Length > 0)
            {
                productModel.ImagePaths = paths.Split(',');
            }
            return productModel;
        }

        public static List<ProductListModel> ConvertProductListToProductModelList(List<Products> productList)
        {
            List<ProductListModel> productModelList = new List<ProductListModel>();
            foreach (Products product in productList)
            {
                ProductListModel newproduct = new ProductListModel()
                {
                    Availability = (bool)product.Availability,
                    ProductDescription = product.ProductDescription,
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductPrice = (decimal)product.ProductPrice,
                    RatingCount = (decimal)product.ProductRating.FirstOrDefault().avgRating,
                    Category = product.SubCategory.Category.name,
                    SubCategory = product.SubCategory.name,
                    NumberOfRating = (int)product.ProductRating.FirstOrDefault().numOfRating,
                    InStock = product.InStock
                };
                string imagePaths = product.ProductImages.FirstOrDefault().uniqueImageName;
                if (imagePaths != null)
                {
                    newproduct.ImagePaths = imagePaths.Split(',');
                }
                productModelList.Add(newproduct);
            }
            return productModelList;
        }

        public static ProductDetailsModel ConvertProductToProductDetailsModel(Products product)
        {
            ProductDetailsModel productDetailsModel = new ProductDetailsModel()
            {
                ProductID = product.ProductID,
                Availability = (bool)product.Availability,
                ProductDescription = product.ProductDescription,
                ProductName = product.ProductName,
                ProductPrice = (decimal)product.ProductPrice,
                RatingNumber = (decimal)product.ProductRating.FirstOrDefault().avgRating
            };
            string imagePaths = product.ProductImages.FirstOrDefault().uniqueImageName;
            if (imagePaths != null)
            {
                productDetailsModel.ImagePaths = imagePaths.Split(',');
            }

            return productDetailsModel;
        }

    }
}
