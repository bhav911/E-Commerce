using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class NewReviewModel
    {
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string Review { get; set; }
        public string RatingNumber { get; set; }
    }
}
