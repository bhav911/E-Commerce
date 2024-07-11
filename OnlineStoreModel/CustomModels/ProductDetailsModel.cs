using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class ProductDetailsModel
    {
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string[] ImagePaths { get; set; }
        public bool Availability { get; set; }
        public decimal RatingNumber { get; set; }
        public List<RatingModel> PublicRatings { get; set; }
        public int InStock { get; set; }

    }
}
