using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class OwnerModel
    {
        public int OwnerID { get; set; }
        public string Shopname { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
