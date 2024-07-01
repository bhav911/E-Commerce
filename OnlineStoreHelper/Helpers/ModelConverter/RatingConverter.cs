using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class RatingConverter
    {
        public static List<RatingModel> ConvertRatingToRatingModel(List<Rating> ratingList)
        {
            List<RatingModel> ratingModelList = new List<RatingModel>();
            foreach (Rating rating in ratingList)
            {
                if (rating.RatingDetails.FirstOrDefault().review == null)
                    continue;
                RatingModel ratingModel = new RatingModel()
                {
                    CustomerName = rating.Customers.username,
                    CustomerID = rating.customerID,
                    HavePurchased = rating.havePurchased,
                    HelpfulCount = rating.RatingDetails.FirstOrDefault().helpfulCount,
                    RatingID = rating.ratingID,
                    RatingNumber = rating.RatingDetails.FirstOrDefault().ratingNumber,
                    Review = rating.RatingDetails.FirstOrDefault().review,
                    ReviewDate = (DateTime)rating.reviewDate
                };

                ratingModel.HelpfulReviewsCustomerID = rating.HelpfulReview.Select(r => r.customerID).ToList();

                ratingModelList.Add(ratingModel);
            }

            return ratingModelList;
        }

        public static RatingModel ConvertRatingToRatingModelSingle(Rating rating)
        {
            RatingModel ratingModel = new RatingModel()
            {
                CustomerName = rating.Customers.username,
                CustomerID = rating.customerID,
                HavePurchased = rating.havePurchased,
                RatingID = rating.ratingID,
                RatingNumber = rating.RatingDetails.FirstOrDefault().ratingNumber,
                Review = rating.RatingDetails.FirstOrDefault().review,
            };

            return ratingModel;
        }
 
    }
}
