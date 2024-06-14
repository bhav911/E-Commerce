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
        public void AddToCart(OrderModel cartOrder)
        {
            CART cart = db.CART.Where(c => c.CustomerID == cartOrder.CustomerID && c.ProductID == cartOrder.ProductID).FirstOrDefault();
            if(cart == null)
            {
                CART newItem = new CART()
                {
                    ProductID = cartOrder.ProductID,
                    Quantity = cartOrder.Quantity,
                    CustomerID = cartOrder.CustomerID
                };
                db.CART.Add(newItem);
            }
            else
            {
                cart.Quantity++;
            }
            db.SaveChanges();
        }
        public List<CART> GetCart(int userID)
        {
            List<CART> cartList = db.CART.Where(c => c.CustomerID == userID).ToList();
            return cartList;
        }
        public void IncrementQuantity(int CartID)
        {
            CART cart = db.CART.Where(c => c.CartID == CartID).FirstOrDefault();
            cart.Quantity++;
            db.SaveChanges();
        }
        public void DecrementQuantity(int CartID)
        {
            CART cart = db.CART.Where(c => c.CartID == CartID).FirstOrDefault();
            if(cart.Quantity > 0)
            {
                cart.Quantity--;
                if(cart.Quantity == 0)
                {
                    db.CART.Remove(cart);
                }
                db.SaveChanges();
            }
        }
        public List<CART> GetOrderDetail(int userID)
        {
            List<CART> cartList = db.CART.Where(c => c.CustomerID == userID).ToList();
            return cartList;
        }
        public void ShiftFromCartToOrders(int userID, int couponApplied)
        {
            List<CART> cartList = db.CART.Where(c => c.CustomerID == userID).ToList();
            decimal subTotal = 0;
            List<OrderDetails> orderDetailList = new List<OrderDetails>();
            foreach (CART item in cartList)
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
                CustomerID = userID,
                Discount = couponApplied == -1 ? 0 : db.Coupons.FirstOrDefault(q => q.CouponID == couponApplied).CouponDiscount,
                SubTotal = subTotal
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
            foreach (CART item in cartList)
            {
                db.CART.Remove(item);
            }
            db.SaveChanges();
        }
    }
}
