using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Select Role")]
        [RegularExpression("^(Admin|Customer)$", ErrorMessage = "Please Select Role")]
        public string Role { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z0-9_]*$", ErrorMessage = "Only alphanumeric and '-' are allowed")]
        [MaxLength(length: 20, ErrorMessage = "Length must be less than 20 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string Login_name { get; set; }

        [Required]
        [MaxLength(length: 20, ErrorMessage = "Length must be less than 20 characters")]
        [MinLength(length: 8, ErrorMessage = "Length must be greater than 8 characters")]
        public string Login_password { get; set; }
    }
}
