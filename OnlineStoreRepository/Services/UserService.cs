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

        public void RegisterUser(Users newUser)
        {
            db.Users.Add(newUser);
            db.SaveChanges();
        }
        public Users AuthenticateUser(LoginModel credentials)
        {
            Users result = db.Users.Where(u => u.username == credentials.Login_name && u.password == credentials.Login_password).FirstOrDefault();
            return result;
        }
    }
}
