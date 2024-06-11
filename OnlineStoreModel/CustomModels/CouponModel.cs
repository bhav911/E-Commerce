using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class CouponModel
    {
        public int CouponID { get; set; }
        public string CouponName { get; set; }
        public decimal CouponDiscount { get; set; }
        public int OwnerID { get; set; }
        public System.DateTime CouponExpiry { get; set; }
        public decimal MinimunPurchase { get; set; }
        public bool Active { get; set; }

    }
}
