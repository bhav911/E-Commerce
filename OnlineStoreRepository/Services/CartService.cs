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

        public void ShiftFromCartToOrders(int userID)
        {
            List<CART> cartList = db.CART.Where(c => c.CustomerID == userID).ToList();
            foreach(CART item in cartList)
            {
                Orders newOrder = new Orders()
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    CustomerID = userID,
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
