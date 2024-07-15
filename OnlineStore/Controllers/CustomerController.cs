using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    [CustomCustomerAuthenticateHelper]
    public class CustomerController : Controller
    {

        public async Task<ActionResult> Showcase()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CustomerApi/Home");
            HomePageModel homePageModel = JsonConvert.DeserializeObject<HomePageModel>(response);
            return View(homePageModel);
        }

        public async Task<ActionResult> Home()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CustomerApi/Home");
            HomePageModel homePageModel = JsonConvert.DeserializeObject<HomePageModel>(response);
            return View(homePageModel);
        }        

        public ActionResult Unauthorize(string role)
        {
            ViewBag.role = role;
            return View();
        }

        public async Task<ActionResult> ShopList()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CustomerApi/ShopList");
            List<OwnerModel> shopModelList = JsonConvert.DeserializeObject<List<OwnerModel>>(response);
            return View(shopModelList);
        }

        public async Task<ActionResult> GetAccount()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CustomerApi/AccountDetails?customerID={UserSession.UserID}");
            CustomerModel customerDetails = JsonConvert.DeserializeObject<CustomerModel>(response);
            return View(customerDetails);
        }

        [HttpPost]
        public ActionResult EditDetails(CustomerModel customerModel)
        {
            return View(customerModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditAccount(CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                customerModel.CustomerID = UserSession.UserID;
                string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/CustomerApi/UpdateProfile", JsonConvert.SerializeObject(customerModel));
                return RedirectToAction("GetAccount"); 
            }
            return View(customerModel);
        }

    }
}