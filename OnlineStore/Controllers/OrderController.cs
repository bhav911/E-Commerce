using Newtonsoft.Json;
using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    public class OrderController : Controller
    {

        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> GetOrdersPlaced()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OrderApi/GetOrdersPlaced?customerID={UserSession.UserID}");
            List<OrderHistoryModel> orderHistoryList = JsonConvert.DeserializeObject<List<OrderHistoryModel>>(response);
            return View(orderHistoryList);
        }


        [HttpPost]
        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> BuyProduct(OrderModel order)
        {
            order.CustomerID = UserSession.UserID;
            string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/OrderApi/BuyProduct", JsonConvert.SerializeObject(order));
            bool status = JsonConvert.DeserializeObject<bool>(response);
            TempData["success"] = "Order Placed Successfully";
            return RedirectToAction("GetOrdersPlaced");
        }
    }
}