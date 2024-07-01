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
    public class RatingController : Controller
    {

        public async Task<JsonResult> LoadMoreReviews(int productID, int reviewNumber)
        {
            List<RatingModel> PublicRatings = await GetUserReviews(productID, reviewNumber);
            return Json(PublicRatings, JsonRequestBehavior.AllowGet);
        }

        public async Task<List<RatingModel>> GetUserReviews(int productID, int reviewNumber)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/RatingApi/GetUserReviews?productID={productID}&reviewNumber={reviewNumber}");
            List<RatingModel> PublicRatings = JsonConvert.DeserializeObject<List<RatingModel>>(response);
            return PublicRatings;
        }

        [HttpPost]
        [CustomUserAuthenticateHelper]
        public async Task<JsonResult> AddReview(NewReviewModel newReview)
        {
            if (newReview.RatingNumber == null)
                return Json(false, JsonRequestBehavior.AllowGet);
            newReview.CustomerID = UserSession.UserID;
            string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/RatingApi/AddReview", JsonConvert.SerializeObject(newReview));
            RatingModel ratingModel = JsonConvert.DeserializeObject<RatingModel>(response);
            TempData["success"] = "Review Added Successfully";
            return Json(ratingModel, JsonRequestBehavior.AllowGet);
        }

        [CustomUserAuthenticateHelper]
        public async Task<JsonResult> ToggleHelpful(int ratingID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/RatingApi/ToggleHelpful?ratingID={ratingID}&customerID={UserSession.UserID}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetFilteredReview(FilteredReviewModel filteredReviewModel)
        {
            List<RatingModel> PublicRatings;
            if (filteredReviewModel.Star == "-1")
            {
                PublicRatings = await GetUserReviews(filteredReviewModel.ProductID, 1);
                return Json(PublicRatings, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/RatingApi/GetFilteredReview", JsonConvert.SerializeObject(filteredReviewModel));
                PublicRatings = JsonConvert.DeserializeObject<List<RatingModel>>(response);
            }

            return Json(PublicRatings, JsonRequestBehavior.AllowGet);
        }
    }
}