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
    public class OrderService : IOrderInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public void AddOrder(OrderModel orderModel)
        {
            decimal productPrice = (decimal)db.Products.FirstOrDefault(q => q.ProductID == orderModel.ProductID).ProductPrice;
            Orders order = new Orders()
            {
                CustomerID = orderModel.CustomerID,
                Discount = orderModel.CouponApplied == -1 ? 0 : db.Coupons.FirstOrDefault(q => q.CouponID == orderModel.CouponApplied).CouponDiscount,
                SubTotal = productPrice * orderModel.Quantity,
                OrderDate = DateTime.Now
            };
            order.TotalPrice = (decimal)(order.SubTotal - (order.SubTotal * (order.Discount / 100)));
            Orders addedOrder = db.Orders.Add(order);
            db.SaveChanges();

            OrderDetails orderDetails = new OrderDetails()
            {
                OrderID = addedOrder.OrderID,
                Quantity = orderModel.Quantity,
                ProductID = orderModel.ProductID,
                unitPrice = productPrice
            };

            db.OrderDetails.Add(orderDetails);
            db.SaveChanges();
        }
        public List<Orders> GetOrderDetail(int userID)
        {
            List<Orders> ordersList = db.Orders.Where(q => q.CustomerID == userID).ToList();
            return ordersList;
        }
    }
}
