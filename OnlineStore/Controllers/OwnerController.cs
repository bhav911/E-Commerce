using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
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
                Products convertedProduct = ModelConverter.ConvertProductModelToProduct(newProduct, UserSession.UserID);
                _product.AddProduct(convertedProduct);
                return RedirectToAction("GetAllProducts");
            }
            return View(newProduct);
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
            Products product = ModelConverter.ConvertProductModelToProduct(productModel, UserSession.UserID);
            product.ProductID = productModel.ProductID;
            _product.EditProduct(product);
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
            List<Orders> ordersRecieved = _owner.GetReceivedOrders(UserSession.UserID);
            List<OrdersReceivedModel> ordersReceivedModels = ModelConverter.ConvertOrdersReceivedToOrdersrecievedModel(ordersRecieved);
            return View(ordersReceivedModels);
        }
    }
}