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
    public class RatingApiController : ApiController
    {
        private readonly RatingServices _rating = new RatingServices();

        [HttpGet]
        [Route("api/RatingApi/GetUserReviews")]
        public List<RatingModel> GetUserReviews(int productID, int reviewNumber)
        {
            List<Rating> rating = _rating.GetAllRatings(productID, reviewNumber);
            List<RatingModel> PublicRatings = RatingConverter.ConvertRatingToRatingModel(rating);
            return PublicRatings;
        }

        [HttpGet]
        [Route("api/RatingApi/ToggleHelpful")]
        public bool ToggleHelpful(int ratingID, int customerID)
        {
            bool status = _rating.ToggleHelpful(ratingID, customerID);
            return status;
        }

        [HttpPost]
        [Route("api/RatingApi/GetFilteredReview")]
        public List<RatingModel> GetFilteredReview(FilteredReviewModel filteredReviewModel)
        {
            List<Rating> rating = _rating.GetFilteredUserReviews(filteredReviewModel);
            List<RatingModel> PublicRatings = RatingConverter.ConvertRatingToRatingModel(rating);
            return PublicRatings;
        }

        [HttpPost]
        [Route("api/RatingApi/AddReview")]
        public RatingModel AddReview(NewReviewModel newReview)
        {
            Rating rating = _rating.AddRating(newReview);
            RatingModel ratingModel = RatingConverter.ConvertRatingToRatingModelSingle(rating);
            return ratingModel;
        }
    }
}