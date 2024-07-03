using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Interface
{
    public interface IRatingInterface
    {
        Rating AddRating(NewReviewModel productRating);
        bool MarkHelpful(int ratingID);
        bool DeleteRating(int ratingID);
        Rating GetRating(int ratingID);
        List<Rating> GetAllRatings(int productID, int reviewNumber);
        List<Rating> GetRatingsOfCustomer(int customerID);
        bool ToggleHelpful(int ratingID, int customerID);
        List<Rating> GetFilteredUserReviews(FilteredReviewModel filteredReviewModel);
    }
}
