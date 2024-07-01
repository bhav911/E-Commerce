using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class NewRegistration
    {
        [Required(ErrorMessage = "Please Select Role")]
        [RegularExpression("^(Owner|Customer)$", ErrorMessage = "Please Select Role")]
        public string Role { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [RegularExpression("^[A-Za-z0-9_ ]*$", ErrorMessage ="Only alphanumeric and '-' are allowed")]
        [MaxLength(length:20, ErrorMessage ="Length must be less than 20 characters")]
        [MinLength(length:8, ErrorMessage ="Length must be greater than 8 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(length: 20, ErrorMessage = "Length must be less than 20 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Password does not match")]
        public string Confirm_password { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Invalid email format")]
        public string Email { get; set; }

        //[RegularExpression("^(Male|Female|Other)$", ErrorMessage ="Invalid")]
        public string Gender { get; set; }

        [Required(ErrorMessage ="Please Select your state")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please Select your City")]
        public int CityID { get; set; }
    }
}
