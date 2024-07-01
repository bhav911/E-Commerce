using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class OrdersReceivedModel
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
