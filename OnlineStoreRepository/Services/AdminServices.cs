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
    public class AdminServices : IAdminInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public ADMINS AuthenticateAdmin(LoginModel credentials)
        {
            ADMINS admin = db.ADMINS.FirstOrDefault(q => q.email == credentials.Login_email && q.password == credentials.Login_password);
            return admin;
        }
    }
}
