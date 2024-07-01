using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers.ModelConverter
{
    public class AdminConverter
    {
        public static AdminModel ConvertAdminToAdminModel(ADMINS admin)
        {
            AdminModel adminModel = new AdminModel()
            {
                AdminID = admin.adminID,
                Email = admin.email,
                Password = admin.password
            };

            return adminModel;
        }
    }
}
