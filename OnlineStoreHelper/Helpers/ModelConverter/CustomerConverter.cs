using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class CustomerConverter
    {
        public static Customers ConvertNewUserToUser(NewRegistration userModel)
        {
            Customers user = new Customers()
            {
                username = userModel.Username,
                CityID = userModel.CityID,
                email = userModel.Email,
                password = userModel.Password,
                StateID = userModel.StateID,
                gender = userModel.Gender == "Male" ? "M" : (userModel.Gender == "Female" ? "F" : "O"),
            };

            return user;
        }

        public static CustomerModel ConvertCustomerToCustomerModel(Customers customer)
        {
            CustomerModel customerModel = new CustomerModel()
            {
                CustomerID = customer.CustomerID,
                Email = customer.email,
                Password = customer.password,
                Username = customer.username,
                Gender = customer.gender,
                CityID = customer.CityID,
                StateID = customer.StateID,
                CityName = customer.Cities.CityName,
                StateName = customer.States.StateName
            };

            return customerModel;
        }
    }
}
