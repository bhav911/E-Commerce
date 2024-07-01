using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreModel.CustomModels
{
    public class FutureDateBlockerAttribute : ValidationAttribute
    {
        public FutureDateBlockerAttribute()
        : base("{0}")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateTime = (DateTime)value;
                if(dateTime < DateTime.Today)
                {
                    var errorMessage = FormatErrorMessage("Please select future date");
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}