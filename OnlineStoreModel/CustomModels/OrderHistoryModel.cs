using OnlineStoreModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class OrderHistoryModel
    {
        public int OrderID { get; set; }
        public decimal SubTotal { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailsModel> OrderDetails { get; set; }
    }
}
