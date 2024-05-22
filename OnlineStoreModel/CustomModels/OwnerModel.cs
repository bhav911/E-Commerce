using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class OwnerModel
    {
        public int ShopID { get; set; }
        public string shopname { get; set; }
        public string description { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
