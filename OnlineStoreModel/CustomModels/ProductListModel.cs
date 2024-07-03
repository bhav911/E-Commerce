using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class ProductListModel
    {
        public int ProductID { get; set; }

        [Required]
        [MaxLength(length: 50, ErrorMessage = "Product name length must be less than 50 characters")]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        [Range(0, 99999, ErrorMessage = "Product price limited to 1 Lakh")]
        public decimal ProductPrice { get; set; }
        public int OwnerID { get; set; }
        public bool Availability { get; set; }
        public string[] ImagePaths { get; set; }
        public decimal RatingCount { get; set; }
        public int NumberOfRating { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public bool? IsActive { get; set; }

    }
}
