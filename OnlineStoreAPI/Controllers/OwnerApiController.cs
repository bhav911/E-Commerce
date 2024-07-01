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
    public class OwnerApiController : ApiController
    {
        private readonly OwnerService _owner = new OwnerService();

        [HttpPost]
        [Route("api/OwnerApi/AuthenticateOwner")]
        public OwnerModel AuthenticateOwner(LoginModel credential)
        {
            Owner owner = _owner.AuthenticateOwner(credential);
            OwnerModel ownerModel = null;
            if (owner != null)
                ownerModel = OwnerConverter.ConvertOwnerToOwnerModel(owner);
            return ownerModel;
        }

        [HttpGet]
        [Route("api/OwnerApi/DoesOwnerExist")]
        public bool DoesOwnerExist(string email)
        {
            Owner owner = _owner.DoesOwnerExist(email);
            return owner != null;
        }
        
        [HttpPost]
        [Route("api/OwnerApi/RegisterOwner")]
        public bool RegisterOwner(NewRegistration newUser)
        {
            try
            {
                Owner owner = OwnerConverter.ConvertNewOwnerToOwner(newUser);
                _owner.RegisterOwner(owner);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        [HttpGet]
        [Route("api/OwnerApi/UploadDocuments")]
        public DocumentModel UploadDocuments(int ownerID)
        {
            DocumentModel docs = _owner.GetDocumentPath(ownerID);
            return docs;
        }

        [HttpPost]
        [Route("api/OwnerApi/UploadDocuments")]
        public bool UploadDocuments(string[] documents, int ownerID)
        {
            try
            {
                _owner.SaveDocuments(documents, ownerID);
                return true;
            }
            catch (Exception)
            {

                return false;
                throw;
            }

        }

        [HttpGet]
        [Route("api/OwnerApi/GetRecievedOrders")]
        public List<OrdersReceivedModel> GetRecievedOrders(int ownerID)
        {
            List<OrderDetails> ordersRecieved = _owner.GetReceivedOrders(ownerID);
            List<OrdersReceivedModel> ordersReceivedModels = OrderConverter.ConvertOrdersReceivedToOrdersrecievedModel(ordersRecieved);
            return ordersReceivedModels;
        }

        [HttpGet]
        [Route("api/OwnerApi/AccountDetails")]
        public OwnerModel AccountDetails(int ownerID)
        {
            Owner owner = _owner.GetOwner(ownerID);
            OwnerModel ownerModel = OwnerConverter.ConvertOwnerToOwnerModel(owner);
            return ownerModel;
        }

        [HttpPost]
        [Route("api/OwnerApi/UpdateProfile")]
        public bool UpdateProfile(OwnerModel ownerModel)
        {
            try
            {
                _owner.UpdateProfile(ownerModel);
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