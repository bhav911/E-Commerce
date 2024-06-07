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
    public class UserService : IUserInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();

        public void RegisterUser(Customers newUser)
        {
            db.Customers.Add(newUser);
            db.SaveChanges();
        }
        public Customers AuthenticateUser(LoginModel credentials)
        {
            Customers result = db.Customers.Where(u => u.email == credentials.Login_email && u.password == credentials.Login_password).FirstOrDefault();
            return result;
        }
    }
}
