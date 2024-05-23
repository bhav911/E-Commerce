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
    public class OwnerService : IOwnerInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public void RegisterOwner(Owner owner)
        {
            db.Owner.Add(owner);
            db.SaveChanges();
        }
        public Owner AuthenticateOwner(LoginModel credentials)
        {
            Owner status = db.Owner.Where(o => o.email == credentials.Login_email && o.password == credentials.Login_password).FirstOrDefault();
            return status;
        }
        public List<Owner> GetAllShops()
        {
            List<Owner> allShops = db.Owner.ToList();
            return allShops;
        }
        public List<Orders> GetReceivedOrders(int ownerID)
        {
            List<Orders> orderList = db.Orders.Where(o => o.Products.ShopID == ownerID).ToList();
            return orderList;
        }
    }
}
