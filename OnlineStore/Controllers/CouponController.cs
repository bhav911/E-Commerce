using Newtonsoft.Json;
using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    public class CouponController : Controller
    {
        
        [CustomAdminAuthentucateHelper]
        public ActionResult AddCoupon()
        {
            return View();
        }

        [HttpPost]
        [CustomAdminAuthentucateHelper]
        public async Task<ActionResult> AddCoupon(CouponModel couponModel)
        {
            if (ModelState.IsValid)
            {
                string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/CouponApi/AddCoupon", JsonConvert.SerializeObject(couponModel));
                bool status = JsonConvert.DeserializeObject<bool>(response);
                TempData["success"] = "Coupon Added Successfully";
                return RedirectToAction("Coupons");
            }
            TempData["error"] = "Please enter valid details";
            return View(couponModel); 
        }

        [ActionName("Coupons")]
        [CustomAdminAuthentucateHelper]
        public async Task<ActionResult> GetCoupons()
        {
            List<CouponModel> couponModelList = await FetchCoupons();
            couponModelList = couponModelList.Where(q => q.CouponExpiry >= DateTime.Today && q.Active).ToList();
            return View(couponModelList);
        }

        public async Task<JsonResult> GetCouponsForOffer()
        {
            List<CouponModel> couponModelList = await FetchCoupons();
            couponModelList = couponModelList.Where(q => q.CouponExpiry >= DateTime.Today && q.Active).ToList();
            return Json(couponModelList, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public async Task<List<CouponModel>> FetchCoupons()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CouponApi/FetchCoupons");
            List<CouponModel> couponModelList = JsonConvert.DeserializeObject<List<CouponModel>>(response);
            return couponModelList;
        }

        [CustomAdminAuthentucateHelper]
        public async Task<JsonResult> ModifyCouponAvailability(int couponID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CouponApi/FetchCoupons?couponID={couponID}");
            bool couponModelList = JsonConvert.DeserializeObject<bool>(response);
            TempData["success"] = "Coupon Availibality Modified Successfully";
            return Json(couponModelList, JsonRequestBehavior.AllowGet);
        }
    }
}