using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStoreModel.CustomModels
{
    public class OwnerModel
    {
        public int OwnerID { get; set; }

        [Required(ErrorMessage = "Shopname is Required")]
        [RegularExpression("^[A-Za-z0-9_ ]*$", ErrorMessage = "Only alphanumeric and '-' are allowed")]
        [MaxLength(length: 50, ErrorMessage = "Length must be less than 50 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string Shopname { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(length: 20, ErrorMessage = "Length must be less than 20 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Please Select your state")]
        public Nullable<int> StateID { get; set; }

        [Required(ErrorMessage = "Please Select your City")]
        public Nullable<int> CityID { get; set; }
    }
}
