using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class CouponConverter
    {
        public static Coupons ConvertCouponModelToCoupon(CouponModel couponModel)
        {
            Coupons coupons = new Coupons()
            {
                Active = couponModel.Active,
                CouponDiscount = couponModel.CouponDiscount,
                CouponExpiry = couponModel.CouponExpiry,
                CouponName = couponModel.CouponName,
                MinimumPurchase = couponModel.MinimunPurchase,
            };
            return coupons;
        }

        public static List<CouponModel> ConvertCouponListToCouponModelList(List<Coupons> couponList)
        {
            List<CouponModel> couponModelList = new List<CouponModel>();
            foreach (Coupons coupon in couponList)
            {
                CouponModel couponModel = new CouponModel()
                {
                    Active = coupon.Active,
                    CouponDiscount = coupon.CouponDiscount,
                    CouponExpiry = (System.DateTime)coupon.CouponExpiry,
                    CouponID = coupon.CouponID,
                    CouponName = coupon.CouponName,
                    MinimunPurchase = (decimal)coupon.MinimumPurchase,
                };

                couponModelList.Add(couponModel);
            }

            return couponModelList;
        }
    }
}
