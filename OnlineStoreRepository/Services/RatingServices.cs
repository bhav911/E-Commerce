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
    public class RatingServices : IRatingInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public bool AddRating(NewReviewModel productRating)
        {
            try
            {
                ProductRating productRatings = db.ProductRating.FirstOrDefault(q => q.productID == productRating.ProductID);
                decimal newAvg = (decimal)(((productRatings.avgRating * productRatings.numOfRating) + Convert.ToInt32(productRating.RatingNumber)) / (productRatings.numOfRating + 1));
                productRatings.avgRating = newAvg;
                productRatings.numOfRating++;
                db.SaveChanges();

                Rating rating = new Rating()
                {
                    customerID = productRating.CustomerID,
                    productID = productRating.ProductID,
                    havePurchased = false,
                    reviewDate = DateTime.Today
                };
                List<Orders> orderList = db.Orders.Where(q => q.CustomerID == productRating.CustomerID).ToList();
                foreach (Orders order in orderList)
                {
                    if (order.OrderDetails.FirstOrDefault(q => q.ProductID == productRating.ProductID) != null)
                    {
                        rating.havePurchased = true;
                        break;
                    }
                }
                rating = db.Rating.Add(rating);
                db.SaveChanges();

                RatingDetails ratingDetails = new RatingDetails()
                {
                    helpfulCount = 0,
                    ratingID = rating.ratingID,
                    ratingNumber = productRating.RatingNumber,
                    review = productRating.Review
                };
                ratingDetails = db.RatingDetails.Add(ratingDetails);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool MarkHelpful(int ratingID)
        {
            try
            {
                RatingDetails ratingDetails = db.RatingDetails.FirstOrDefault(q => q.ratingID == ratingID);
                ratingDetails.helpfulCount++;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeleteRating(int ratingID)
        {
            try
            {
                RatingDetails ratingDetails = db.RatingDetails.FirstOrDefault(q => q.ratingID == ratingID);
                db.RatingDetails.Remove(ratingDetails);
                Rating rating = db.Rating.FirstOrDefault(q => q.ratingID == ratingID);
                db.Rating.Remove(rating);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public Rating GetRating(int ratingID)
        {
            Rating rating = db.Rating.FirstOrDefault(q => q.ratingID == ratingID);
            return rating;
        }
        public List<Rating> GetAllRatings(int productID, int reviewNumber)
        {
            int max = 3;
            List<Rating> ratingList = db.Rating.Where(q => q.productID == productID).OrderBy(m => m.ratingID).Skip((reviewNumber - 1) * max).Take(max).ToList();
            return ratingList;
        }
        public List<Rating> GetRatingsOfCustomer(int customerID)
        {
            List<Rating> ratingList = db.Rating.Where(q => q.customerID == customerID).ToList();
            return ratingList;
        }
        public bool ToggleHelpful(int ratingID, int customerID)
        {
            try
            {
                HelpfulReview helpfulReview = db.HelpfulReview.FirstOrDefault(q => q.ratingID == ratingID && q.customerID == customerID);
                RatingDetails ratingDetails = db.RatingDetails.FirstOrDefault(r => r.ratingID == ratingID);
                if (helpfulReview == null) { 
                    ratingDetails.helpfulCount++;
                    HelpfulReview newHelpfulReview = new HelpfulReview()
                    {
                        customerID = customerID,
                        ratingID = ratingID
                    };
                    db.HelpfulReview.Add(newHelpfulReview);
                }
                else
                {
                    ratingDetails.helpfulCount--;
                    db.HelpfulReview.Remove(helpfulReview);
                }
                db.SaveChanges();
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
