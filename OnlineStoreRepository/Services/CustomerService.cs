using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class CustomerService : ICustomerInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();

        public void RegisterCustomer(Customers newUser)
        {
            Customers customer = db.Customers.Add(newUser);
            db.SaveChanges();
            Cart cart = new Cart()
            {
                CustomerID = customer.CustomerID,
                ItemCount = 0
            };
            db.Cart.Add(cart);
            db.SaveChanges();
        }
        public Customers AuthenticateCustomer(LoginModel credentials)
        {
            Customers result = db.Customers.Where(u => u.email == credentials.Login_email && u.password == credentials.Login_password).FirstOrDefault();
            return result;
        }

        public Customers DoesCustomerExist(string email)
        {
            Customers customer = db.Customers.FirstOrDefault(q => q.email == email);
            return customer;
        }

        public  Customers GetCustomer(int customerID)
        {
            Customers customer = db.Customers.FirstOrDefault(q => q.CustomerID == customerID);
            return customer;
        }

        public bool UpdateProfile(CustomerModel customerModel)
        {
            try
            {
                Customers customer = db.Customers.FirstOrDefault(q => q.CustomerID == customerModel.CustomerID);
                customer.username = customerModel.Username;
                customer.email = customerModel.Email;
                customer.CityID = customerModel.CityID;
                customer.StateID = customerModel.StateID;
                customer.password = customerModel.Password;
                customer.gender = customerModel.Gender == "Male" ? "M" : (customerModel.Gender == "Female" ? "F" : "O"); 
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}
