using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Interface;
using QuizComputation_490.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class ProductService : IProductInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public void AddProduct(Products newProduct, string aggregatedProductImages)
        {
            Products products = db.Products.Add(newProduct);
            db.SaveChanges();
            ProductImages productImages = new ProductImages()
            {
                ProductID = products.ProductID,
                uniqueImageName = aggregatedProductImages
            };
            productImages = db.ProductImages.Add(productImages);
            ProductRating productRating = new ProductRating()
            {
                avgRating = 0,
                numOfRating = 0,
                productID = productImages.ProductID
            };
            db.ProductRating.Add(productRating);
            db.SaveChanges();
        }

        public void EditProduct(EditProductModel editProductModel)
        {
            Products newProductInfo = editProductModel.Product;
            Products product = db.Products.Where(p => p.ProductID == newProductInfo.ProductID).FirstOrDefault();
            product.ProductName = newProductInfo.ProductName;
            product.ProductDescription = newProductInfo.ProductDescription;
            product.ProductPrice = newProductInfo.ProductPrice;
            product.subCategoryID = newProductInfo.subCategoryID;

            Dictionary<string, object> kvp = new Dictionary<string, object>();
            if(editProductModel.ImageFileToDelete != null)
            {
                foreach (string fileName in editProductModel.ImageFileToDelete)
                {
                    kvp.Clear();
                    kvp.Add("@imageID", editProductModel.ImgID);
                    kvp.Add("@imageName", fileName);
                    kvp.Add("@imageLen", fileName.Length);
                    SqlSPHelper.SqlSPConnector("DeleteImagePath", kvp);
                }
            }

            if(editProductModel.AggregatedImagePathToAdd.Length > 0)
            {
                kvp.Clear();
                kvp.Add("@imageID", editProductModel.ImgID);
                kvp.Add("@imageName", editProductModel.AggregatedImagePathToAdd);
                DataTable result = SqlSPHelper.SqlSPConnector("UpdateFilePath", kvp);
            }
            db.SaveChanges();
        }

        public Products GetProduct(int prodID)
        {
            Products product = db.Products.Where(p => p.ProductID == prodID).FirstOrDefault();
            return product;
        }

        public List<Products> GetAllProducts(int OwnerID)
        {
            List<Products> productList = db.Products.Where(s => s.OwnerID == OwnerID && (bool)!s.isDeleted).ToList();
            return productList;
        }

        public bool DeleteProduct(int prodID)
        {
            try
            {
                Products productToDelete = db.Products.FirstOrDefault(p => p.ProductID == prodID);
                //ProductImages productImages = db.ProductImages.FirstOrDefault(p => p.ProductID == prodID);
                //ProductRating productRating = db.ProductRating.FirstOrDefault(p => p.productID == prodID);
                //List<Rating> rating = productToDelete.Rating.ToList();
                //foreach(Rating rt in rating)
                //{
                //    db.RatingDetails.Remove(rt.RatingDetails.FirstOrDefault());
                //}
                //db.Rating.RemoveRange(rating);
                //db.ProductImages.Remove(productImages);
                //db.ProductRating.Remove(productRating);
                //db.Products.Remove(Products productToDelete = db.Products.FirstOrDefault(p => p.ProductID == prodID);

                productToDelete.isDeleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Products> GetProductsOfSubCategory(int subCategoryID)
        {
            List<Products> productList = db.Products.Where(q => q.subCategoryID == subCategoryID && !(bool)q.isDeleted).ToList();
            return productList;
        }

        public List<Products> GetProductsOfCategory(int categoryID)
        {
            List<Products> productList = db.Products.Where(q => q.SubCategory.categoryID == categoryID && !(bool)q.isDeleted).ToList();
            return productList;
        }

        public bool ToggleProductActiveness(int productID)
        {
            try
            {
                Products products = db.Products.FirstOrDefault(q => q.ProductID == productID);
                products.Availability = !products.Availability;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
