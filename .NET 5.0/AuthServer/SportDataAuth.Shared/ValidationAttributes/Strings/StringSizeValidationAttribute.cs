using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared
{
    public class StringSizeValidationAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public StringSizeValidationAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string currentFieldValue = ValidationRequests.FieldValueToString(value);

            if(ValidationRequests.Required(currentFieldValue))
            {
                if (currentFieldValue.Count() < _min || currentFieldValue.Count() > _max)
                    return new ValidationResult($"{validationContext.DisplayName} must be between {_min} and {_max} characters");
            }
            return ValidationResult.Success;
        }
    }
}
