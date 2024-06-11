using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class CouponService : ICouponInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public Coupons AddCoupon(Coupons coupon){
            try
            {
                Coupons addedCoupon = db.Coupons.Add(coupon);
                db.SaveChanges();
                return addedCoupon;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public List<Coupons> GetCoupons(){
            try
            {
                List<Coupons> couponList = db.Coupons.ToList();
                return couponList;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public bool ModifyCouponAvailabilty(int couponID)
        {
            try
            {
                Coupons coupon = db.Coupons.FirstOrDefault(q => q.CouponID == couponID);
                coupon.Active = !coupon.Active;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
