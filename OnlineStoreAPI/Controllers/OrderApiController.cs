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
    public class OrderApiController : ApiController
    {
        private readonly OrderService _order = new OrderService();

        [HttpGet]
        [Route("api/OrderApi/GetOrdersPlaced")]
        public List<OrderHistoryModel> GetOrdersPlaced(int customerID)
        {
            List<Orders> orderPlacedList = _order.GetOrderDetail(customerID);
            List<OrderHistoryModel> orderPlacedModelList = OrderConverter.ConvertOrderListToOrderModelList(orderPlacedList);
            return orderPlacedModelList;
        }

        [HttpPost]
        [Route("api/OrderApi/BuyProduct")]
        public bool BuyProduct(OrderModel orderModel)
        {
            try
            {
                _order.AddOrder(orderModel);
                return true;
            }
            catch (Exception)
            {

                return false;
                throw;
            }    
        }
    }
}