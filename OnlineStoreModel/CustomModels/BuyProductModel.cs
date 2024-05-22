using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class OrderModel
    {
        public int productID { get; set; }
        public int quantity { get; set; }
        public int userID { get; set; }
    }
}
