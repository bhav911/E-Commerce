using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class FilterProductModel
    {
        public int? Rating { get; set; } = 0;
        public bool Availability { get; set; }
        public int? LowPrice { get; set; }
        public int? HighPrice { get; set; } = 200000;
        public int subcategoryID { get; set; }
    }
}
