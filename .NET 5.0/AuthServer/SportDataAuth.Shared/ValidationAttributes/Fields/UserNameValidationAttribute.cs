using SportDataAuth.Shared.AuthRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared
{
    public class UserNameValidationAttribute : ValidationAttribute
    {
        private readonly bool _required;

        public UserNameValidationAttribute(bool required)
        {
            _required = required;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string UsernameMustBeBetween5And25Characters = "Username must be between 5 and 25 characters";
            string SpecialCharactersAreNotAllowed = "Special characters are not allowed";

            string currentFieldValue = ValidationRequests.FieldValueToString(value);
            bool check = ValidationRequests.Required(currentFieldValue);

            if (_required && check == false)
            {
                return ValidationRequests.FieldIsRequired(validationContext);
            }
            else if (check)
            {
                if (currentFieldValue.Length < 5 || currentFieldValue.Length > 25)
                {
                    if (!ValidationRequests.UserNameValidation(currentFieldValue))
                    {
                        return new ValidationResult(UsernameMustBeBetween5And25Characters + " and " + SpecialCharactersAreNotAllowed);
                    };
                    return new ValidationResult(UsernameMustBeBetween5And25Characters);
                }

                if (!ValidationRequests.UserNameValidation(currentFieldValue))
                    return new ValidationResult(SpecialCharactersAreNotAllowed);
            }
            return ValidationResult.Success;
        }
    }
}
