using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class FilteredReviewModel
    {

        public FilteredReviewModel()
        {
            ReviewNumber = 1;
        }

        public string Star { get; set; }
        public int ProductID { get; set; }
        public int ReviewNumber {get; set;}
    }
}
