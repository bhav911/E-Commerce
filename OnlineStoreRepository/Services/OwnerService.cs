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
            owner = db.Owner.Add(owner);
            OwnerKYC ownerkyc = new OwnerKYC()
            {
                OwnerID = owner.OwnerID
            };
            db.OwnerKYC.Add(ownerkyc);
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

        public Owner DoesOwnerExist(string email)
        {
            Owner owner = db.Owner.FirstOrDefault(q => q.email == email);
            return owner;
        }

        public List<OrderDetails> GetReceivedOrders(int ownerID)
        {
            List<OrderDetails> orderList = db.OrderDetails.Where(q => q.Products.OwnerID == ownerID).ToList();
            return orderList;
        }
        public void SaveDocuments(string[] docs, int userID)
        {
            Owner owner = db.Owner.FirstOrDefault(u => u.OwnerID == userID);
            OwnerKYC ownerkyc = owner.OwnerKYC.FirstOrDefault();
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
            docs.DocPaths[0] = ownerKyc.panCard?? null;
            docs.DocPaths[1] = ownerKyc.aadharCard?? null;
            docs.DocPaths[2] = ownerKyc.passpostImage?? null;
            docs.DocPaths[3] = ownerKyc.shopImage?? null;
            return docs;
        }

        public Owner GetOwner(int ownerID)
        {
            Owner owner = db.Owner.FirstOrDefault(q => q.OwnerID == ownerID);
            return owner;
        }

        public bool UpdateProfile(OwnerModel ownerModel)
        {
            try
            {
                Owner owner = db.Owner.FirstOrDefault(q => q.OwnerID == ownerModel.OwnerID);
                owner.shopname = ownerModel.Shopname;
                owner.CityID = ownerModel.CityID;
                owner.StateID = ownerModel.StateID;
                owner.email = ownerModel.Email;
                owner.Description = ownerModel.Description;
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
