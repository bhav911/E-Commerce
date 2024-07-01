using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class CartService : ICartInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public Cart AddToCart(OrderModel cartOrder)
        {
            Cart cart = db.Cart.FirstOrDefault(c => c.CustomerID == cartOrder.CustomerID);
            cart.ItemCount++;
            CartItems cartItem = cart.CartItems.FirstOrDefault(q => q.ProductID == cartOrder.ProductID);
            if(cartItem == null)
            {
                CartItems newItem = new CartItems()
                {
                    ProductID = cartOrder.ProductID,
                    Quantity = 1,
                    CartID = cart.CartID
                };
                db.CartItems.Add(newItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            db.SaveChanges();
            return cart;
        }
        public List<CartItems> GetCart(int CustomerID)
        {
            Cart cart = db.Cart.FirstOrDefault(q => q.CustomerID == CustomerID);
            if (cart.ItemCount == 0)
                return new List<CartItems>();
            List <CartItems> cartList = cart.CartItems.ToList();
            return cartList;
        }
        public CartItems IncrementQuantity(int CartItemID)
        {
            CartItems cartItem = db.CartItems.FirstOrDefault(c => c.CartItemID == CartItemID);
            cartItem.Quantity++;
            db.SaveChanges();
            return cartItem;
        }
        public CartItems DecrementQuantity(int CartItemID)
        {
            CartItems cartItem = db.CartItems.FirstOrDefault(c => c.CartItemID == CartItemID); 
            if (cartItem.Quantity > 0)
            {
                cartItem.Quantity--;
                if(cartItem.Quantity == 0)
                {
                    cartItem.Cart.ItemCount--;
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
            return cartItem;
        }
        public List<CartItems> GetOrderDetail(int CustomerID)
        {
            Cart cart = db.Cart.FirstOrDefault(q => q.CustomerID == CustomerID);
            List<CartItems> cartItemList = cart.CartItems.ToList();
            return cartItemList;
        }
        public Orders ShiftFromCartToOrders(int CustomerID, int couponApplied)
        {
            Cart cart = db.Cart.FirstOrDefault(q => q.CustomerID == CustomerID);
            cart.ItemCount = 0;
            List<CartItems> cartItemList = cart.CartItems.ToList();
            decimal subTotal = 0;
            List<OrderDetails> orderDetailList = new List<OrderDetails>();
            foreach (CartItems item in cartItemList)
            {
                subTotal += (decimal)(item.Quantity * item.Products.ProductPrice);
                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    unitPrice = (decimal)(item.Products.ProductPrice)
                };
                orderDetailList.Add(orderDetails);
            }
            Orders order = new Orders()
            {
                CustomerID = CustomerID,
                Discount = couponApplied == -1 ? 0 : db.Coupons.FirstOrDefault(q => q.CouponID == couponApplied).CouponDiscount,
                SubTotal = subTotal,
                OrderDate = DateTime.Now
            };
            order.TotalPrice = (decimal)(order.SubTotal - (order.SubTotal * (order.Discount / 100)));

            Orders addedOrder = db.Orders.Add(order);
            db.SaveChanges();
            foreach(OrderDetails orderDetails in orderDetailList)
            {
                orderDetails.OrderID = addedOrder.OrderID;
            }
            db.OrderDetails.AddRange(orderDetailList);
            db.SaveChanges();            
            foreach (CartItems item in cartItemList)
            {
                db.CartItems.Remove(item);
            }
            db.SaveChanges();
            return addedOrder;
        }
    }
}
