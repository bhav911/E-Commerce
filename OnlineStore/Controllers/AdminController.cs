using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly CouponService _coupon = new CouponService();
        public ActionResult AddCoupon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCoupon(CouponModel couponModel)
        {
            Coupons coupon = ModelConverter.ConvertCouponModelToCoupon(couponModel, UserSession.UserID);
            Coupons addedCoupon = _coupon.AddCoupon(coupon);
            return RedirectToAction("AddCoupon");
        }

        [ActionName("Coupons")]
        public ActionResult GetCoupons()
        {
            List<CouponModel> couponModelList = FetchCoupons();
            return View(couponModelList);
        }

        public JsonResult GetCouponsForOffer()
        {
            List<CouponModel> couponModelList = FetchCoupons().Where(q => q.CouponExpiry >= System.DateTime.Today && q.Active).ToList();
            return Json(couponModelList, JsonRequestBehavior.AllowGet);
        }

        public List<CouponModel> FetchCoupons()
        {
            List<Coupons> couponList = _coupon.GetCoupons();
            List<CouponModel> couponModelList = ModelConverter.ConvertCouponListToCouponModelList(couponList);
            return couponModelList;
        }

        public JsonResult ModifyCouponAvailability(int couponID)
        {
            bool status = _coupon.ModifyCouponAvailabilty(couponID);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}