using Newtonsoft.Json;
using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult SignIn()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(LoginModel credential)
        {
            if (ModelState.IsValid)
            {
                string encr = EncryptionDecryptionHelper.Encrypt(credential.Login_password);
                string decr = EncryptionDecryptionHelper.Decrypt(encr);


                if (credential.Role == "Owner")
                {
                    string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/OwnerApi/AuthenticateOwner", JsonConvert.SerializeObject(credential));
                    Owner owner = JsonConvert.DeserializeObject<Owner>(response);
                    if (owner != null)
                    {
                        TempData["Role"] = "Owner";
                        UserSession.UserID = owner.OwnerID;
                        UserSession.Username = owner.shopname;
                        UserSession.UserRole = credential.Role;
                        TempData["success"] = "Logged In Successfully";
                        return RedirectToAction("Dashboard", "Owner");
                    }
                }
                else if(credential.Role == "Customer")
                {
                    string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/CustomerApi/AuthenticateCustomer", JsonConvert.SerializeObject(credential));
                    Customers customer = JsonConvert.DeserializeObject<Customers>(response);
                    if (customer != null)
                    {
                        TempData["Role"] = "Customer";
                        UserSession.UserID = customer.CustomerID;
                        UserSession.Username = customer.username;
                        UserSession.UserRole = credential.Role;
                        TempData["success"] = "Logged In Successfully";
                        return RedirectToAction("Home", "Customer");
                    }
                }
                else if (credential.Role == "Admin")
                {
                    string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/AdminApi/AuthenticateAdmin", JsonConvert.SerializeObject(credential));
                    ADMINS admin = JsonConvert.DeserializeObject<ADMINS>(response);
                    if (admin != null)
                    {
                        TempData["Role"] = "Admin";
                        UserSession.UserID = admin.adminID;
                        UserSession.Username = admin.email;
                        UserSession.UserRole = credential.Role;
                        TempData["success"] = "Logged In Successfully";
                        return RedirectToAction("Coupons", "Coupon");
                    }
                }
            }
            TempData["error"] = "Invalid Credentials";

            return View(credential);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(NewRegistration newUser)
        {
            if (ModelState.IsValid)
            {
                string response;
                bool status;
                if (newUser.Role == "Owner")
                {
                    response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OwnerApi/DoesOwnerExist?email={newUser.Email}");
                    status = JsonConvert.DeserializeObject<bool>(response);
                    if (!status)
                    {
                        response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/OwnerApi/RegisterOwner", JsonConvert.SerializeObject(newUser));
                        status = JsonConvert.DeserializeObject<bool>(response);
                        TempData["success"] = "Registered Successfully";
                        return RedirectToAction("SignIn");
                    }
                    TempData["error"] = "Entered email is already registered to another account";
                    return View(newUser);
                }
                else if (newUser.Role == "Customer")
                {
                    response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CustomerApi/DoesCustomerExist?email={newUser.Email}");
                    status = JsonConvert.DeserializeObject<bool>(response);
                    if (!status)
                    {
                        response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/CustomerApi/RegisterCustomer", JsonConvert.SerializeObject(newUser));
                        TempData["success"] = "Registered Successfully";
                        return RedirectToAction("SignIn");
                    }
                    TempData["error"] = "Entered email is already registered to another account";
                    return View(newUser);
                }
            }
            TempData["error"] = "Please enter valid data";
            return View(newUser);
        }

        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction("SignIn");
        }

        public async Task<JsonResult>  GetStates()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/LocationApi/GetStates");
            List<StateModel> listOfState = JsonConvert.DeserializeObject<List<StateModel>>(response);
            return Json(listOfState, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCities(int stateID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/LocationApi/GetCities?stateID={stateID}");
            List<CityModel> listOfCities = JsonConvert.DeserializeObject<List<CityModel>>(response);
            return Json(listOfCities, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Error()
        {
            if(UserSession.UserID == 0)
            {
                return RedirectToAction("SignIn");
            }
            return View();
        }
    }
}