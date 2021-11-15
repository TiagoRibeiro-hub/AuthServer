using SportDataAuth.Shared.AuthRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared
{
    public class PassWordValidationAttribute : ValidationAttribute
    {
        private readonly bool _required;

        public PassWordValidationAttribute(bool required)
        {
            _required = required;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int upperCase = 2; int lowerCase = 2;
            int number = 2; int specialChar = 2;

            string currentFieldValue = ValidationRequests.FieldValueToString(value);
            bool check = ValidationRequests.Required(currentFieldValue);

            if (_required && check == false)
            {
                return ValidationRequests.FieldIsRequired(validationContext);
            }
            else if (check)
            {
                for (int i = 0; i < currentFieldValue.Length; i++)
                {
                    var x = currentFieldValue[i];
                    if (x >= 65 && x <= 90)
                        upperCase = -1;
                        if (x >= 97 && x <= 122)
                            lowerCase = -1;
                        if (x >= 48 && x <= 57)
                            number = -1;
                            if ((x >= 33 && x <= 47) || (x >= 58 && x <= 64) || (x >= 91 && x <= 96) || (x >= 123 && x <= 126))
                                specialChar = -1;
                }

                if (!ValidationRequests.PassWordValidation(currentFieldValue))
                {
                    if (currentFieldValue.Length < 8)
                    {
                        return new ValidationResult("Password must be at least 8 characters, 2 special characters, 2 Uppercase, 2 Lowercase, and 2 numbers");
                    }
                }

                if (upperCase > 0 || lowerCase > 0 || number > 0 || specialChar > 0)
                    return new ValidationResult("Password must be at least 2 special characters, 2 Uppercase, 2 Lowercase, and 2 numbers");
            }
            return ValidationResult.Success;
        }

    }
}
