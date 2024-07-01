using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class CouponModel
    {
        public int CouponID { get; set; }

        [Required(ErrorMessage = "Coupon Name is Required")]
        [RegularExpression("^[A-Z0-9]*$", ErrorMessage = "Only Upper case alphanumeric are allowed")]
        [MaxLength(length: 20, ErrorMessage = "Length must be less than 20 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string CouponName { get; set; }

        [Required(ErrorMessage = "Discount is Required")]
        [Range(0, 100, ErrorMessage ="Discound should be greater than 0 and lesser than 100 %")]
        public decimal CouponDiscount { get; set; }
        public int OwnerID { get; set; }

        [Required(ErrorMessage = "Expiry date is Required")]
        [FutureDateBlocker]
        public DateTime CouponExpiry { get; set; }

        [Required(ErrorMessage = "Min purchase is Required")]
        public decimal MinimunPurchase { get; set; }
        public bool Active { get; set; }

    }
}
