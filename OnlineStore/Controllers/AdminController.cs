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
        public JsonResult GetCoupons()
        {
            List<Coupons> couponList = _coupon.GetCoupons();
            List<CouponModel> couponModelList = ModelConverter.ConvertCouponListToCouponModelList(couponList);
            return Json(couponModelList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModifyCouponAvailability(int couponID)
        {
            bool status = _coupon.ModifyCouponAvailabilty(couponID);
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}