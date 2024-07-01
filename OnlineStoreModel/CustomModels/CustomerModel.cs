using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [RegularExpression("^[A-Za-z0-9_ ]*$", ErrorMessage = "Only alphanumeric and '-' are allowed")]
        [MaxLength(length: 20, ErrorMessage = "Length must be less than 20 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(length: 20, ErrorMessage = "Length must be less than 20 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Please Select Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Select your state")]
        public Nullable<int> StateID { get; set; }

        [Required(ErrorMessage = "Please Select your City")]
        public Nullable<int> CityID { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
}
