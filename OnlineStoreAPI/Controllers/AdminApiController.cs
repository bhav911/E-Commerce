using OnlineStoreHelper.Helpers.ModelConverter;
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
    public class AdminApiController : ApiController
    {
        private readonly AdminServices _admin = new AdminServices();

        [HttpPost]
        [Route("api/AdminApi/AuthenticateAdmin")]
        public AdminModel AuthenticateAdmin(LoginModel credential)
        {
            ADMINS admin = _admin.AuthenticateAdmin(credential);
            AdminModel adminModel = null;
            if(admin != null)
                adminModel = AdminConverter.ConvertAdminToAdminModel(admin);
            return adminModel;
        }
    }
}