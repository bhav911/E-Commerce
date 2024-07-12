using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class OrderConverter
    {
        public static List<OrderHistoryModel> ConvertOrderListToOrderModelList(List<Orders> orderPlacedList)
        {
            List<OrderHistoryModel> orderModelList = new List<OrderHistoryModel>();
            foreach (Orders order in orderPlacedList)
            {
                OrderHistoryModel current = new OrderHistoryModel()
                {
                    SubTotal = order.SubTotal,
                    Discount = order.Discount,
                    TotalPrice = order.TotalPrice,
                    OrderDetails = new List<OrderDetailsModel>(),
                    OrderID = order.OrderID
                };

                List<OrderDetails> orderDetailList = order.OrderDetails.ToList();
                foreach (OrderDetails orderDetails in orderDetailList)
                {
                    OrderDetailsModel orderModel = new OrderDetailsModel()
                    {
                        ProductName = orderDetails.Products.ProductName,
                        ProductPrice = orderDetails.unitPrice,
                        ProductQuantity = (int)orderDetails.Quantity,
                        TotalPrice = (decimal)(orderDetails.unitPrice * orderDetails.Quantity),
                        ProductID = (int)orderDetails.ProductID
                    };
                    current.OrderDetails.Add(orderModel);
                }
                orderModelList.Add(current);
            }
            return orderModelList.OrderByDescending(q => q.OrderID).ToList();
        }

        public static List<OrdersReceivedModel> ConvertOrdersReceivedToOrdersrecievedModel(List<OrderDetails> ordersRecieved)
        {
            List<OrdersReceivedModel> ordersReceivedModel = new List<OrdersReceivedModel>();

            foreach (OrderDetails order in ordersRecieved)
            {
                OrdersReceivedModel orderRecieved = new OrdersReceivedModel()
                {
                    ProductName = order.Products.ProductName,
                    ProductQuantity = (int)order.Quantity,
                    UserEmail = order.Orders.Customers.email,
                    OrderDate = (DateTime)order.Orders.OrderDate
                };
                ordersReceivedModel.Add(orderRecieved);
            }

            return ordersReceivedModel;
        }
    }
}
