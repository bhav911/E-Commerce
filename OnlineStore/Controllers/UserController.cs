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
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    [CustomUserAuthenticateHelper]
    public class UserController : Controller
    {
        private readonly OwnerService _owner = new OwnerService();
        private readonly ProductService _product = new ProductService();
        private readonly OrderService _order = new OrderService();
        private readonly CartService _cart = new CartService();
        private readonly CouponService _coupon = new CouponService();
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
                Quantity = 1,
                CustomerID = UserSession.UserID,
                ProductID = productID
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
            order.CustomerID = UserSession.UserID;
            _order.AddOrder(order);
            return RedirectToAction("GetAllProducts", new { shopID });
        }

        public ActionResult ViewCart()
        {
            List<CART> cartList = _cart.GetCart(UserSession.UserID);
            List<CartModel> cartModelList = ModelConverter.ConvertCartListToCartListModel(cartList); 
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

        public ActionResult GetOrderDetails()
        {
            List<CART> orderDetailList = _cart.GetOrderDetail(UserSession.UserID);
            List<CartModel> orderDetailModelList = ModelConverter.ConvertCartListToCartListModel(orderDetailList);
            List<Coupons> couponList = _coupon.GetCoupons();
            List<CouponModel> couponModelList = ModelConverter.ConvertCouponListToCouponModelList(couponList);
            CartCouponModel cartCouponModel = new CartCouponModel()
            {
                CartModelList = orderDetailModelList,
                CouponModelList = couponModelList
            };
            cartCouponModel.TotalPrice = CalculateTotalPrice(orderDetailModelList);
            return View(cartCouponModel);
        }

        [NonAction]
        private decimal CalculateTotalPrice(List<CartModel> orderDetailModelList)
        {
            decimal total = 0;
            foreach(var item in orderDetailModelList)
            {
                total += item.ProductQuantity * item.ProductPrice;
            }

            return total;
        }

        public ActionResult GetOrdersPlaced()
        {
            List<Orders> orderPlacedList = _order.GetOrderDetail(UserSession.UserID);
            List<OrderHistoryModel> orderPlacedModelList = ModelConverter.ConvertOrderListToOrderModelList(orderPlacedList);
            return View(orderPlacedModelList);
        }

        [HttpPost]
        public ActionResult Checkout(int couponApplied = -1)
        {
                _cart.ShiftFromCartToOrders(UserSession.UserID, couponApplied);
            return RedirectToAction("ShopList");
        }

        public ActionResult Unauthorize(string role)
        {
            ViewBag.role = "Customer";
            return View();
        }

    }
}