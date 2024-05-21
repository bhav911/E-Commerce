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
            return View();
        }


        [HttpPost]
        public ActionResult SignIn(LoginModel credential)
        {
            if (ModelState.IsValid)
            {
                if (credential.Role == "Admin")
                {
                    Owner owner = _owner.AuthenticateOwner(credential);
                    if (owner != null)
                        return View("SignUp");
                }
                else if(credential.Role == "Customer")
                {
                    Users user = _user.AuthenticateUser(credential);
                    if (user != null)
                        return View("SignUp");
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
                    return View("SignIn");
                }
                else if (newUser.Role == "Customer")
                {
                    Users user = ModelConverter.ConvertNewUserToUser(newUser);
                    _user.RegisterUser(user);
                    return View("SignIn");
                }
            }
            return View(newUser);
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