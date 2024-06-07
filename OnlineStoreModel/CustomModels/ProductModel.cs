using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineStoreModel.CustomModels
{
    public class ProductModel
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
        public List<HttpPostedFileBase> productImages { get; set; }
        public string[] ImagePaths { get; set; }
        public int? ImageID { get; set; }
    }
}
