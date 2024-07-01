using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class CartModel
    {
        public int CartID { get; set; }
        public int CustomerID { get; set; }
        public int ItemCount { get; set; }
    }
}
