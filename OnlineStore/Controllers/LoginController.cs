using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly OwnerService _owner = new OwnerService();
        private readonly UserService _user = new UserService();
        private readonly StateCityService _stateCity = new StateCityService();
        public ActionResult SignIn()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginModel credential)
        {
            if (ModelState.IsValid)
            {
                string encr = EncryptionDecryptionHelper.Encrypt(credential.Login_password);
                string decr = EncryptionDecryptionHelper.Decrypt(encr);


                if (credential.Role == "Owner")
                {
                    Owner owner = _owner.AuthenticateOwner(credential);
                    if (owner != null)
                    {
                        TempData["Role"] = "Owner";
                        UserSession.UserID = owner.OwnerID;
                        UserSession.Username = owner.shopname;
                        UserSession.UserRole = credential.Role;
                        return RedirectToAction("GetAllProducts", "Owner");
                    }
                }
                else if(credential.Role == "Customer")
                {
                    Customers user = _user.AuthenticateUser(credential);
                    if (user != null)
                    {
                        TempData["Role"] = "Customer";
                        UserSession.UserID = user.CustomerID;
                        UserSession.Username = user.username;
                        UserSession.UserRole = credential.Role;
                        return RedirectToAction("ShopList", "User");
                    }
                }
            }

            return View(credential);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(NewRegistration newUser)
        {
            if (ModelState.IsValid)
            {
                if (newUser.Role == "Admin")
                {
                    Owner owner = ModelConverter.ConvertNewOwnerToOwner(newUser);
                    _owner.RegisterOwner(owner);
                    return RedirectToAction("SignIn");
                }
                else if (newUser.Role == "Customer")
                {
                    Customers user = ModelConverter.ConvertNewUserToUser(newUser);
                    _user.RegisterUser(user);
                    return RedirectToAction("SignIn");
                }
            }
            return View(newUser);
        }

        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction("SignIn");
        }

        public JsonResult  GetStates()
        {
            List<StateModel> listOfState = _stateCity.GetStates();
            return Json(listOfState, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities(int stateID)
        {
            List<CityModel> listOfCities = _stateCity.GetCities(stateID);
            return Json(listOfCities, JsonRequestBehavior.AllowGet);
        }
    }
}