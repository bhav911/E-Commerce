using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class CartCouponModel
    {
        public decimal TotalPrice { get; set; }
        public List<CartItemModel> CartModelList { get; set; }
        public List<CouponModel> CouponModelList { get; set; }
    }
}
