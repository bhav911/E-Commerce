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
    public class UserController : Controller
    {
        private readonly OwnerService _owner = new OwnerService();
        private readonly ProductService _product = new ProductService();
        private readonly OrderService _order = new OrderService();
        private readonly CartService _cart = new CartService();
        public ActionResult ShopList()
        {
            List<Owner> shopList = _owner.GetAllShops();
            List<OwnerModel> shopModelList = ModelConverter.ConvertOwnerToOwnerModel(shopList);
            return View(shopModelList);
        }

        public ActionResult GetAllProducts(int shopID)
        {
            List<Products> productList = _product.GetAllProducts(shopID);
            List<ProductModel> productModelList = ModelConverter.ConvertProductListToProductModelList(productList);
            return View(productModelList);
        }

        public JsonResult AddProductToCart(int productID)
        {
            OrderModel order = new OrderModel()
            {
                quantity = 1,
                userID = UserSession.UserID,
                productID = productID
            };
            _cart.AddToCart(order);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuyProduct(int productID)
        {
            Products product = _product.GetProduct(productID);
            ProductModel productModel = ModelConverter.ConvertProductToProductModel(product);
            return View(productModel);
        }

        [HttpPost]
        public ActionResult BuyProduct(OrderModel order, int shopID)
        {
            order.userID = UserSession.UserID;
            _order.AddOrder(order);
            return RedirectToAction("GetAllProducts", new { shopID });
        }

        public ActionResult ViewCart()
        {
            List<CartModel> cartModelList = _cart.GetCart(UserSession.UserID);
            return View(cartModelList);
        }

        public JsonResult IncrementQuantity(int cartID)
        {
            _cart.IncrementQuantity(cartID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DecrementQuantity(int cartID)
        {
            _cart.DecrementQuantity(cartID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        ////public ActionResult CheckOut()
        ////{

        ////}
    }
}