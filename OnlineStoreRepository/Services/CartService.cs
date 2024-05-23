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
            CART cart = db.CART.Where(c => c.userID == cartOrder.userID && c.productID == cartOrder.productID).FirstOrDefault();
            if(cart == null)
            {
                CART newItem = new CART()
                {
                    productID = cartOrder.productID,
                    quantity = cartOrder.quantity,
                    userID = cartOrder.userID
                };
                db.CART.Add(newItem);
            }
            else
            {
                cart.quantity++;
            }
            db.SaveChanges();
        }
        public List<CART> GetCart(int userID)
        {
            List<CART> cartList = db.CART.Where(c => c.userID == userID).ToList();
            return cartList;
        }
        public void IncrementQuantity(int cartID)
        {
            CART cart = db.CART.Where(c => c.cartID == cartID).FirstOrDefault();
            cart.quantity++;
            db.SaveChanges();
        }
        public void DecrementQuantity(int cartID)
        {
            CART cart = db.CART.Where(c => c.cartID == cartID).FirstOrDefault();
            if(cart.quantity > 0)
            {
                cart.quantity--;
                if(cart.quantity == 0)
                {
                    db.CART.Remove(cart);
                }
                db.SaveChanges();
            }
        }
        public List<CART> GetOrderDetail(int userID)
        {
            List<CART> cartList = db.CART.Where(c => c.userID == userID).ToList();
            return cartList;
        }

        public void ShiftFromCartToOrders(int userID)
        {
            List<CART> cartList = db.CART.Where(c => c.userID == userID).ToList();
            foreach(CART item in cartList)
            {
                Orders newOrder = new Orders()
                {
                    ProductID = item.productID,
                    Quantity = item.quantity,
                    UserID = userID,
                    unitPrice = (decimal)item.Products.ProductPrice
                };
                db.Orders.Add(newOrder);
            }
            foreach (CART item in cartList)
            {
                db.CART.Remove(item);
            }
            db.SaveChanges();
        }
    }
}
