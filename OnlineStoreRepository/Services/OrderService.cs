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
            Orders order = new Orders()
            {
                ProductID = orderModel.ProductID,
                CustomerID = orderModel.CustomerID,
                Quantity = orderModel.Quantity,
                unitPrice = (decimal)orderModel.ProductPrice
            };
            var status = db.Orders.Add(order);
            db.SaveChanges();
        }
        public List<Orders> GetOrderDetail(int userID)
        {
            List<Orders> ordersList = db.Orders.Where(o => o.CustomerID == userID).ToList();
            return ordersList;
        }
    }
}
