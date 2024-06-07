using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class OrderModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int CustomerID { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
