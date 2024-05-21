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
    public class OwnerController : Controller
    {
        private readonly ProductService _product = new ProductService();
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
            newProduct.ShopID = 1;
            if (ModelState.IsValid)
            {
                Products convertedProduct = ModelConverter.ConvertProductModelToProduct(newProduct);
                _product.AddProduct(convertedProduct);
                return View();
            }
            return View();
        }

        //public ActionResult EditProduct()
        //{

        //}

        //public ActionResult DeleteProduct()
        //{

        //}

        //public ActionResult GetProduct(int productID)
        //{

        //}

        //public ActionResult GetAllProducts()
        //{

        //}
    }
}