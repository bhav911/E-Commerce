using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class CartConverter
    {
        public static List<CartItemModel> ConvertCartListToCartListModel(List<CartItems> cartItemList)
        {
            List<CartItemModel> cartItemModelList = new List<CartItemModel>();

            foreach (CartItems cartItem in cartItemList)
            {
                CartItemModel cartModel = new CartItemModel()
                {
                    ProductName = cartItem.Products.ProductName,
                    ProductPrice = (decimal)cartItem.Products.ProductPrice,
                    ProductQuantity = cartItem.Quantity,
                    CartItemID = cartItem.CartItemID
                };

                cartItemModelList.Add(cartModel);
            }

            return cartItemModelList;
        }

        public static CartModel ConvertCartToCartModel(Cart cart)
        {
            CartModel cartModel = new CartModel()
            {
                CartID = cart.CartID,
                CustomerID = cart.CustomerID,
                ItemCount = cart.ItemCount
            };

            return cartModel;
        }
        public static CartItemModel ConvertCartItemToCartItemModel(CartItems cartItem)
        {
            CartItemModel cartItemModel = new CartItemModel()
            {
                CartItemID = cartItem.CartItemID,
                ProductName = cartItem.Products.ProductName,
                ProductPrice = (decimal)cartItem.Products.ProductPrice,
                ProductQuantity = cartItem.Quantity,
                InStock = cartItem.Products.InStock
            };

            return cartItemModel;
        }
    }
}
