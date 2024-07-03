using Newtonsoft.Json;
using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    [CustomCustomerAuthenticateHelper]
    public class CartController : Controller
    {
        public async Task<JsonResult> AddProductToCart(int productID)
        {
            OrderModel order = new OrderModel()
            {
                CustomerID = UserSession.UserID,
                ProductID = productID
            };
            string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest("api/CartApi/AddProductToCart", JsonConvert.SerializeObject(order));
            CartModel cartModel = JsonConvert.DeserializeObject<CartModel>(response);
            return Json(cartModel, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewCart()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CartApi/ViewCart?customerID={UserSession.UserID}");
            List<CartItemModel> cartList = JsonConvert.DeserializeObject<List<CartItemModel>>(response);
            return View(cartList);
        }

        public async Task<JsonResult> IncrementQuantity(int cartID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CartApi/IncrementQuantity?cartItemID={cartID}");
            CartItems cartList = JsonConvert.DeserializeObject<CartItems>(response);
            return Json(cartList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DecrementQuantity(int cartID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CartApi/DecrementQuantity?cartItemID={cartID}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Checkout(int couponApplied = -1)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CartApi/Checkout?customerID={UserSession.UserID}&couponApplied={couponApplied}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            TempData["success"] = "Order Placed Successfully";
            return RedirectToAction("GetOrdersPlaced", "Order");
        }

        public async Task<ActionResult> GetOrderDetails()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CartApi/GetOrderDetails?customerID={UserSession.UserID}");
            CartCouponModel cartCouponModel = JsonConvert.DeserializeObject<CartCouponModel>(response);
            return View(cartCouponModel);
        }
    }
}