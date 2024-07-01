using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineStoreAPI.Controllers
{
    public class CouponApiController : ApiController
    {
        private readonly CouponService _coupon = new CouponService();

        [HttpPost]
        [Route("api/CouponApi/AddCoupon")]
        public bool AddCoupon(CouponModel couponModel)
        {
            try
            {
                Coupons coupon = CouponConverter.ConvertCouponModelToCoupon(couponModel);
                Coupons addedCoupon = _coupon.AddCoupon(coupon);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        [HttpGet]
        [Route("api/CouponApi/FetchCoupons")]
        public List<CouponModel> FetchCoupons()
        {
            List<Coupons> couponList = _coupon.GetCoupons();
            List<CouponModel> couponModelList = CouponConverter.ConvertCouponListToCouponModelList(couponList);
            return couponModelList;
        }

        [HttpGet]
        [Route("api/CouponApi/ModifyCouponAvailability")]
        public bool ModifyCouponAvailability(int couponID)
        {
            try
            {
                bool status = _coupon.ModifyCouponAvailabilty(couponID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }            
        }
    }
}