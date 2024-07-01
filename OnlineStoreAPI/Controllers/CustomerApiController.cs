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
    public class CustomerApiController : ApiController
    {
        private readonly CategoryServices _category = new CategoryServices();
        private readonly OwnerService _owner = new OwnerService();
        private readonly CustomerService _customer = new CustomerService();

        [HttpGet]
        [Route("api/CustomerApi/Home")]
        public HomePageModel Home()
        {
            List<Category> categorieList = _category.GetAllCategories();
            List<CategoryModel> categoryModelList = CategoryConverter.ConvertCategoryListToCategoryModelListForHomePage(categorieList);
            HomePageModel homePageModel = new HomePageModel()
            {
                CategoryList = categoryModelList
            };
            return homePageModel;
        }

        [HttpGet]
        [Route("api/CustomerApi/ShopList")]
        public List<OwnerModel> ShopList()
        {
            List<Owner> shopList = _owner.GetAllShops();
            List<OwnerModel> shopModelList = OwnerConverter.ConvertOwnerListToOwnerModelList(shopList);
            return shopModelList;
        }

        [HttpPost]
        [Route("api/CustomerApi/AuthenticateCustomer")]
        public CustomerModel AuthenticateCustomer(LoginModel credential)
        {
            Customers customer = _customer.AuthenticateCustomer(credential);
            CustomerModel customerModel = null;
            if (customer != null)
            customerModel = CustomerConverter.ConvertCustomerToCustomerModel(customer);
            return customerModel;
        }

        [HttpGet]
        [Route("api/CustomerApi/DoesCustomerExist")]
        public bool DoesCustomerExist(string email)
        {
            Customers customer = _customer.DoesCustomerExist(email);
            return customer != null;
        }

        [HttpPost]
        [Route("api/CustomerApi/RegisterCustomer")]
        public bool RegisterCustomer(NewRegistration newUser)
        {
            try
            {
                Customers customer = CustomerConverter.ConvertNewUserToUser(newUser);
                _customer.RegisterCustomer(customer);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        [HttpGet]
        [Route("api/CustomerApi/AccountDetails")]
        public CustomerModel AccountDetails(int customerID)
        {
            Customers customer = _customer.GetCustomer(customerID);
            CustomerModel customerModel = CustomerConverter.ConvertCustomerToCustomerModel(customer);
            return customerModel;
        }

        [HttpPost]
        [Route("api/CustomerApi/UpdateProfile")]
        public bool UpdateProfile(CustomerModel customerModel)
        {
            try
            {
                _customer.UpdateProfile(customerModel);
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