using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class FilterOrderModel
    {
        public List<OrdersReceivedModel> OrderList { get; set; }
        public List<ProductModel> ProductList { get; set; }
    }
}
