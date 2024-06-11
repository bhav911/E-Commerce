using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    [CustomOwnerAuthentucateHelper]
    public class OwnerController : Controller
    {
        private readonly ProductService _product = new ProductService();
        private readonly OwnerService _owner = new OwnerService();

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel newProduct)
        {
            if (ModelState.IsValid)
            {
                string aggregatedProductImages = "";
                foreach(HttpPostedFileBase file in newProduct.productImages)
                {
                    aggregatedProductImages += GetUniqueFileName(file) + ",";
                }
                if(aggregatedProductImages.Length > 0)
                    aggregatedProductImages = aggregatedProductImages.Substring(0, aggregatedProductImages.Length - 1);
                Products convertedProduct = ModelConverter.ConvertProductModelToProduct(newProduct, UserSession.UserID);
                _product.AddProduct(convertedProduct, aggregatedProductImages);
                return RedirectToAction("GetAllProducts");
            }
            return View(newProduct);
        }

       

        private string GetUniqueFileName(HttpPostedFileBase file)
        {
            if (file == null)
                return null;
            string ext = Path.GetExtension(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/ProductImages/") + uniqueFileName);
            if (ext.Equals(".pdf")){
                file.SaveAs(HttpContext.Server.MapPath("~/Content/KYC/PDFs/") + uniqueFileName);
            }
            else{
                file.SaveAs(HttpContext.Server.MapPath("~/Content/KYC/IMGs/") + uniqueFileName);
            }
            return uniqueFileName;
        }

        public ActionResult EditProduct(int productID)
        {
            Products product = _product.GetProduct(productID);
            ProductModel productModel = ModelConverter.ConvertProductToProductModel(product);
            return View("AddProduct", productModel);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductModel productModel)
        {
            string[] imageFileToDelete = null;
            if (productModel.ImagePaths != null && productModel.ImagePaths[0].Length > 0)
            {
                productModel.ImagePaths[0] = productModel.ImagePaths[0].Substring(1);
                imageFileToDelete = productModel.ImagePaths[0].Split(',');
            }
            string aggregatedImagePathToAdd = "";
            if(productModel.productImages != null)
            {
                foreach (HttpPostedFileBase file in productModel.productImages)
                {
                    if (file == null)
                        continue;
                    aggregatedImagePathToAdd += GetUniqueFileName(file) + ",";
                }
                aggregatedImagePathToAdd = aggregatedImagePathToAdd.Substring(0, aggregatedImagePathToAdd.Length - 1);
            }
            Products product = ModelConverter.ConvertProductModelToProduct(productModel, UserSession.UserID);
            product.ProductID = productModel.ProductID;
            _product.EditProduct(product, aggregatedImagePathToAdd, imageFileToDelete, (int)productModel.ImageID);
            return RedirectToAction("getAllProducts");
        }

        public ActionResult DeleteProduct(int productID)
        {
            bool status = _product.DeleteProduct(productID);
            return RedirectToAction("GetAllProducts");
        }

        public ActionResult GetAllProducts()
        {
            List<Products> productList = _product.GetAllProducts(UserSession.UserID);
            List<ProductModel> productModelList = ModelConverter.ConvertProductListToProductModelList(productList);
            return View(productModelList);
        }

        public ActionResult GetRecievedOrders()
        {
            List<OrderDetails> ordersRecieved = _owner.GetReceivedOrders(UserSession.UserID);
            List<OrdersReceivedModel> ordersReceivedModels = ModelConverter.ConvertOrdersReceivedToOrdersrecievedModel(ordersRecieved);
            return View(ordersReceivedModels);
        }

        public ActionResult Unauthorize(string role)
        {
            ViewBag.role = "Owner";
            return View();
        }

        public ActionResult UploadDocuments()
        {
            DocumentModel docs = _owner.GetDocumentPath(UserSession.UserID);
            return View(docs);
        }
        [HttpPost]
        public ActionResult UploadDocuments(DocumentModel docs)
        {
            if (ModelState.IsValid)
            {
                docs.DocPaths[0] = GetUniqueFileName(docs.PanCard);
                docs.DocPaths[1] = GetUniqueFileName(docs.AadharCard);
                docs.DocPaths[2] = GetUniqueFileName(docs.PassportImage);
                docs.DocPaths[3] = GetUniqueFileName(docs.ShopImage);
                _owner.SaveDocuments(docs.DocPaths,UserSession.UserID);
                return RedirectToAction("GetAllProducts");
            }
            return RedirectToAction("UploadDocuments");
        }

    }
}