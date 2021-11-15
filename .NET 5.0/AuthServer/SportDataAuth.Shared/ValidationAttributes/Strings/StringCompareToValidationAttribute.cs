using SportDataAuth.Shared.AuthRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared
{
    public class StringCompareToValidationAttribute : ValidationAttribute
    {
        private readonly string _stringToCompare;

        public StringCompareToValidationAttribute(string stringToCompare)
        {
            _stringToCompare = stringToCompare;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = ValidationRequests.PropertyValue(validationContext, _stringToCompare);
            string propertyValue = ValidationRequests.PropertyValueToString(propertyInfo);

            string currentFieldValue = ValidationRequests.FieldValueToString(value);

            if(!string.IsNullOrEmpty(propertyValue))
            {
                if(ValidationRequests.Required(currentFieldValue))
                {
                    if (ValidationRequests.StringCompareTo(currentFieldValue, propertyValue))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult($"{validationContext.DisplayName} needs to be equal to {_stringToCompare}");
                }
                return ValidationRequests.FieldIsRequired(validationContext);
            }
            return ValidationRequests.PropertyValueEmpty(_stringToCompare);

        }

    }
}

