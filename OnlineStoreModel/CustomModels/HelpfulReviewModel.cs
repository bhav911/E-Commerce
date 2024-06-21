using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class HelpfulReviewModel
    {
        public int helpfulReviewID { get; set; }
        public int ratingID { get; set; }
        public int productID { get; set; }
        public int customerID { get; set; }
    }
}
