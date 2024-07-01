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
    public class ProductApiController : ApiController
    {
        private readonly ProductService _product = new ProductService();
        private readonly RatingServices _rating = new RatingServices();

        [HttpPost]
        [Route("api/ProductApi/AddProduct")]
        public bool AddProduct(Products newProduct, string aggregatedProductImages)
        {
            try
            {
                _product.AddProduct(newProduct, aggregatedProductImages);
                return true;
            }
            catch (Exception)
            {

                return false;
                throw;
            }
        }

        [HttpGet]
        [Route("api/ProductApi/EditProduct")]
        public ProductModel EditProduct(int productID)
        {
            Products product = _product.GetProduct(productID);
            ProductModel productModel = ProductConverter.ConvertProductToProductModel(product);
            return productModel;
        }

        [HttpPost]
        [Route("api/ProductApi/EditProduct")]
        public bool EditProduct(EditProductModel editProductModel)
        {
            try
            {
                _product.EditProduct(editProductModel);
                return true;
            }
            catch (Exception)
            {

                return false;
                throw;
            }
        }

        [HttpGet]
        [Route("api/ProductApi/DeleteProduct")]
        public bool DeleteProduct(int productID)
        {
            try
            {
                bool status = _product.DeleteProduct(productID);
                return true;
            }
            catch (Exception)
            {

                return false;
                throw;
            }
        }

        [HttpGet]
        [Route("api/ProductApi/GetMyProducts")]
        public List<ProductListModel> GetMyProducts(int ownerID)
        {
            List<Products> productList = _product.GetAllProducts(ownerID);
            List<ProductListModel> productModelList = ProductConverter.ConvertProductListToProductModelList(productList);
            return productModelList;
        }

        [HttpGet]
        [Route("api/ProductApi/GetProduct")]
        public ProductModel GetProduct(int productID)
        {
            Products product = _product.GetProduct(productID);
            ProductModel productModel = ProductConverter.ConvertProductToProductModel(product);
            return productModel;
        }

        [HttpGet]
        [Route("api/ProductApi/GetProductDetails")]
        public ProductDetailsModel GetProductDetails(int productID, int customerID)
        {
            Products product = _product.GetProduct(productID);
            ProductDetailsModel productDetailsModel = ProductConverter.ConvertProductToProductDetailsModel(product);
            productDetailsModel.CustomerID = customerID;
            List<Rating> rating = GetUserReviews(productID, 1);
            productDetailsModel.PublicRatings = RatingConverter.ConvertRatingToRatingModel(rating);
            return productDetailsModel;
        }

        [NonAction]
        public List<Rating> GetUserReviews(int productID, int reviewNumber)
        {
            List<Rating> rating = _rating.GetAllRatings(productID, reviewNumber);
            return rating;
        }

        [HttpGet]
        [Route("api/ProductApi/GetProductsOfSubCategory")]
        public List<ProductListModel> GetProductsOfSubCategory(int subCategoryID)
        {
            List<Products> productList = _product.GetProductsOfSubCategory(subCategoryID);
            List<ProductListModel> productModelList = ProductConverter.ConvertProductListToProductModelList(productList);
            return productModelList;
        }

        [HttpGet]
        [Route("api/ProductApi/GetProductsOfCategory")]
        public List<ProductListModel> GetProductsOfCategory(int categoryID)
        {
            List<Products> productList = _product.GetProductsOfCategory(categoryID);
            List<ProductListModel> productModelList = ProductConverter.ConvertProductListToProductModelList(productList);
            return productModelList;
        }
    }
}