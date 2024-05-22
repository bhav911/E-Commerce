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
            CART newItem = new CART()
            {
                productID = cartOrder.productID,
                quantity = cartOrder.quantity,
                userID = cartOrder.userID
            };

            db.CART.Add(newItem);
            db.SaveChanges();
        }
        public List<CartModel> GetCart(int userID)
        {
            List<CartModel> cartList = db.CART
                .Where(c => c.userID == userID)
                .Join(db.Users, c => c.userID, u => u.UserID, (c, u) => new { c, u })
                .Join(db.Products, cu => cu.c.productID, p => p.ProductID, (cu, p) => new { cu.c, cu.u, p })
                .GroupBy(cp => new { cp.p.ProductName, cp.p.ProductPrice })
                .Select(g => new CartModel
                {
                    productName = g.Key.ProductName,
                    productPrice = (decimal)g.Key.ProductPrice,
                    productQuantity = g.Sum(x => x.c.quantity),
                })
                .ToList();
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
            cart.quantity--;
            db.SaveChanges();
        }
    }
}
