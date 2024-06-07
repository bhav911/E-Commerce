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
            List<Orders> orderList = db.Orders.Where(o => o.Products.OwnerID == ownerID).ToList();
            return orderList;
        }
        public void SaveDocuments(string[] docs, int userID)
        {
            Owner owner = db.Owner.FirstOrDefault(u => u.OwnerID == userID);
            OwnerKYC ownerkyc = owner.OwnerKYC.FirstOrDefault();
            if(ownerkyc == null)
            {
                ownerkyc = new OwnerKYC()
                {
                    OwnerID = userID
                };
            }
            if(docs[0] != null)
                ownerkyc.panCard = docs[0];
            if (docs[1] != null)
                ownerkyc.aadharCard = docs[1];
            if (docs[2] != null)
                ownerkyc.passpostImage = docs[2];
            if (docs[3] != null)
                ownerkyc.shopImage = docs[3];
            db.SaveChanges();
        }

        public DocumentModel GetDocumentPath(int userID)
        {
            OwnerKYC ownerKyc = db.OwnerKYC.FirstOrDefault(u => u.OwnerID == userID);
            DocumentModel docs = new DocumentModel();
            docs.DocPaths[0] = ownerKyc.panCard;
            docs.DocPaths[1] = ownerKyc.aadharCard;
            docs.DocPaths[2] = ownerKyc.passpostImage;
            docs.DocPaths[3] = ownerKyc.shopImage;
            return docs;
        }
    }
}
