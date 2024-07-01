using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class FutureDateBlockerAttribute : ValidationAttribute
    {
        public FutureDateBlockerAttribute()
        : base("{0} contains invalid character.")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateTime = (DateTime)value;
            }
            return ValidationResult.Success;
        }
    }
}
