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
    public class CartApiController : ApiController
    {
        private readonly CouponService _coupon = new CouponService();
        private readonly CartService _cart = new CartService();

        [HttpPost]
        [Route("api/CartApi/AddProductToCart")]
        public CartModel AddProductToCart(OrderModel orderModel)
        {
            Cart cart = _cart.AddToCart(orderModel);
            if (cart == null)
                return null;
            CartModel cartModel = CartConverter.ConvertCartToCartModel(cart);
            return cartModel;
        }

        [HttpGet]
        [Route("api/CartApi/ViewCart")]
        public List<CartItemModel> ViewCart(int customerID)
        {
            List<CartItems> cartItemList = _cart.GetCart(customerID);
            List<CartItemModel> cartModelList = CartConverter.ConvertCartListToCartListModel(cartItemList);
            return cartModelList;
        }

        [HttpGet]
        [Route("api/CartApi/IncrementQuantity")]
        public CartItemModel IncrementQuantity(int cartItemID)
        {
            try
            {
                CartItems cartItem = _cart.IncrementQuantity(cartItemID);
                CartItemModel cartItemModel = CartConverter.ConvertCartItemToCartItemModel(cartItem);
                return cartItemModel;
            }
            catch (Exception)
            {
                return new CartItemModel();
                throw;
            }
        }

        [HttpGet]
        [Route("api/CartApi/DecrementQuantity")]
        public bool DecrementQuantity(int cartItemID)
        {
            CartItems cartItem = _cart.DecrementQuantity(cartItemID);
            return true;
        }

        [HttpGet]
        [Route("api/CartApi/RemoveCart")]
        public bool RemoveCart(int cartItemID)
        {
            bool status = _cart.RemoveCart(cartItemID);
            return status;
        }

        [HttpGet]
        [Route("api/CartApi/Checkout")]
        public bool Checkout(int couponApplied, int customerID)
        {
            try
            {
                Orders order = _cart.ShiftFromCartToOrders(customerID, couponApplied);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        [HttpGet]
        [Route("api/CartApi/GetOrderDetails")]
        public CartCouponModel GetOrderDetails(int customerID)
        {
            List<CartItems> orderDetailList = _cart.GetOrderDetail(customerID);
            if(orderDetailList == null)
            {
                return null;
            }
            List<CartItemModel> orderDetailModelList = CartConverter.ConvertCartListToCartListModel(orderDetailList);
            List<Coupons> couponList = _coupon.GetCoupons();
            List<CouponModel> couponModelList = CouponConverter.ConvertCouponListToCouponModelList(couponList);
            CartCouponModel cartCouponModel = new CartCouponModel()
            {
                CartModelList = orderDetailModelList,
                CouponModelList = couponModelList.Where(q => q.CouponExpiry >= DateTime.Today && q.Active).ToList()
            };
            cartCouponModel.TotalPrice = CalculateTotalPrice(orderDetailModelList);
            return cartCouponModel;
        }

        [NonAction]
        private decimal CalculateTotalPrice(List<CartItemModel> orderDetailModelList)
        {
            decimal total = 0;
            foreach (var item in orderDetailModelList)
            {
                total += item.ProductQuantity * item.ProductPrice;
            }

            return total;
        }

    }
}