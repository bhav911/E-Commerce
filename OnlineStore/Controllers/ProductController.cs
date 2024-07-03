using Newtonsoft.Json;
using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    public class ProductController : Controller
    {

        [CustomOwnerAuthentucateHelper]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> AddProduct(ProductModel newProduct)
        {
            if (ModelState.IsValid)
            {
                string aggregatedProductImages = "";
                if(newProduct.ProductImages != null)
                {
                    foreach (HttpPostedFileBase file in newProduct.ProductImages)
                    {
                        aggregatedProductImages += GetUniqueFileName(file) + ",";
                    }
                }
                if (aggregatedProductImages.Length > 0)
                    aggregatedProductImages = aggregatedProductImages.Substring(0, aggregatedProductImages.Length - 1);
                Products convertedProduct = ProductConverter.ConvertProductModelToProduct(newProduct, UserSession.UserID);
                string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/ProductApi/AddProduct?aggregatedProductImages={aggregatedProductImages}", JsonConvert.SerializeObject(convertedProduct));
                TempData["success"] = "Product Added Successfully";
                return RedirectToAction("GetMyProducts");
            }
            return View(newProduct);
        }

        [NonAction]
        private string GetUniqueFileName(HttpPostedFileBase file)
        {
            if (file == null)
                return null;
            string ext = Path.GetExtension(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/ProductImages/") + uniqueFileName);
            if (ext.Equals(".pdf"))
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Content/KYC/PDFs/") + uniqueFileName);
            }
            else
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Content/KYC/IMGs/") + uniqueFileName);
            }
            return uniqueFileName;
        }

        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> EditProduct(int productID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/EditProduct?productID={productID}");
            ProductModel productModel = JsonConvert.DeserializeObject<ProductModel>(response);
            return View("AddProduct", productModel);
        }

        [HttpPost]
        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> EditProduct(ProductModel productModel)
        {
            string[] imageFileToDelete = null;
            if (productModel.ImagePaths != null && productModel.ImagePaths[0].Length > 0)
            {
                productModel.ImagePaths[0] = productModel.ImagePaths[0].Substring(1);
                imageFileToDelete = productModel.ImagePaths[0].Split(',');
            }
            string aggregatedImagePathToAdd = "";
            if (productModel.ProductImages != null)
            {
                foreach (HttpPostedFileBase file in productModel.ProductImages)
                {
                    if (file == null)
                        continue;
                    aggregatedImagePathToAdd += GetUniqueFileName(file) + ",";
                }
                aggregatedImagePathToAdd = aggregatedImagePathToAdd.Substring(0, aggregatedImagePathToAdd.Length - 1);
            }
            Products product = ProductConverter.ConvertProductModelToProduct(productModel, UserSession.UserID);
            product.ProductID = productModel.ProductID;
            EditProductModel editProductModel = new EditProductModel
            {
                Product = product,
                AggregatedImagePathToAdd = aggregatedImagePathToAdd,
                ImageFileToDelete = imageFileToDelete,
                ImgID = (int)productModel.ImageID
            };
            string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/ProductApi/EditProduct", JsonConvert.SerializeObject(editProductModel));
            bool status = JsonConvert.DeserializeObject<bool>(response);
            TempData["success"] = "Product Edited Successfully";
            return RedirectToAction("GetMyProducts");
        }

        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> DeleteProduct(int productID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/DeleteProduct?productID={productID}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("GetMyProducts");
        }

        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> GetMyProducts()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetMyProducts?ownerID={UserSession.UserID}");
            List<ProductListModel> productModelList = JsonConvert.DeserializeObject<List<ProductListModel>>(response);
            return View(productModelList);
        }

        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> GetAllProducts()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetMyProducts?ownerID={UserSession.UserID}");
            List<ProductListModel> productModelList = JsonConvert.DeserializeObject<List<ProductListModel>>(response);
            return View(productModelList);
        }

        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> BuyProduct(int productID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetProduct?productID={productID}");
            ProductModel productModel = JsonConvert.DeserializeObject<ProductModel>(response);
            return View(productModel);
        }

        public async Task<ActionResult> GetProductDetails(int productID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetProductDetails?productID={productID}&customerID={UserSession.UserID}");
            ProductDetailsModel productDetailsModel = JsonConvert.DeserializeObject<ProductDetailsModel>(response);
            return View(productDetailsModel);
        }

        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> GetMyProductDetails(int productID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetProductDetails?productID={productID}&customerID={UserSession.UserID}");
            ProductDetailsModel productDetailsModel = JsonConvert.DeserializeObject<ProductDetailsModel>(response);
            return View(productDetailsModel);
        }

        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> GetProductsOfSubCategory(int subCategoryID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetProductsOfSubCategory?subCategoryID={subCategoryID}");
            List<ProductListModel> productModelList = JsonConvert.DeserializeObject<List<ProductListModel>>(response);
            return View("GetAllProducts", productModelList);
        }

        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> GetProductsOfCategory(int categoryID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetProductsOfCategory?categoryID={categoryID}");
            List<ProductListModel> productModelList = JsonConvert.DeserializeObject<List<ProductListModel>>(response);
            return View("GetAllProducts", productModelList);
        }

        [CustomOwnerAuthentucateHelper]
        public async Task<JsonResult> ToggleProductActiveness(int productID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/ToggleProductActiveness?productID={productID}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}