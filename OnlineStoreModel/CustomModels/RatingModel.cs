using OnlineStoreModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class RatingModel
    {
        public int RatingID { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        public bool? HavePurchased { get; set; }      
        public int RatingDetailsID { get; set; }
        public string Review { get; set; }
        public string RatingNumber { get; set; }
        public int? HelpfulCount { get; set; }
        public List<int> HelpfulReviewsCustomerID { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
