using SportDataAuth.Shared.AuthRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        private readonly bool _required;

        public PhoneNumberValidationAttribute(bool required)
        {
            _required = required;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string currentFieldValue = ValidationRequests.FieldValueToString(value);
            bool check = ValidationRequests.Required(currentFieldValue);

            if (_required && check == false)
            {
                return ValidationRequests.FieldIsRequired(validationContext);
            }
            else if (check)
            {
                if (!ValidationRequests.PhoneNumberValidation(currentFieldValue))
                    return new ValidationResult("Invalid phone number");
            }
            return ValidationResult.Success;
        }
    }
}
