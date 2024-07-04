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

        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> GetRecievedOrders(DateTime? startDate, DateTime? endDate, int? productID)
        {
            if(startDate != null && endDate != null && startDate > endDate || startDate > DateTime.Today || endDate > DateTime.Today)
            {
                TempData["error"] = "Please select appropriate date";
                return RedirectToAction("GetRecievedOrders");
            }
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OwnerApi/GetRecievedOrders?ownerID={UserSession.UserID}&startDate={startDate}&endDate={endDate}&productID={productID}");
            List<OrdersReceivedModel> ordersReceivedModels = JsonConvert.DeserializeObject<List<OrdersReceivedModel>>(response);
            response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetMyProducts?ownerID={UserSession.UserID}");
            List<ProductModel> productList = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            FilterOrderModel filterOrderModel = new FilterOrderModel()
            {
                OrderList = ordersReceivedModels,
                ProductList = productList
            };
            TempData["startDate"] = startDate;
            TempData["endDate"] = endDate;
            TempData["productID"] = productID;
            return View("../Owner/GetRecievedOrders", filterOrderModel);
        }

        public ActionResult GeneratePDF(DateTime? startDate, DateTime? endDate, int? productID)
        {
            var orderReport = new Rotativa.ActionAsPdf("GetRecievedOrders", new { startDate, endDate, productID});
            return orderReport;
        }
    }
}