using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportDataAuth.Shared
{
    public static class ValidationRequests
    {
        // Validations
        #region Validations

        public static bool Required(string x)
        {
            if (!string.IsNullOrWhiteSpace(x))
                return true;

            return false;
        }

        public static bool EmailValidation(string email)
        {
            Regex validar = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return validar.IsMatch(email);
        }

        public static bool UserNameValidation(string userName)
        {
            // LettersAndNumbersIsValid
            Regex validar = new Regex(@"^[0-9a-zA-ZãáàéêíóõúçÃÁÀÉÊÍÓÕÚÇºª.,\s]+$");
            return validar.IsMatch(userName);
        }

        public static bool PhoneNumberValidation(string phoneNumber)
        {
            Regex validar = new Regex(@"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$");
            return validar.IsMatch(phoneNumber);
        }

        public static bool PassWordValidation(string password)
        {
            // min 8 char letter e number
            Regex validar = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            return validar.IsMatch(password);
        }

        public static bool StringCompareTo(string x, string y)
        {
            if (x == y)
                return true;

            return false;
        }

        public static bool JustLettersIsValid(string x)
        {
            Regex validar = new Regex(@"^[a-zA-ZãáàéêíóõúçÃÁÀÉÊÍÓÕÚÇ\s]+$");
            return validar.IsMatch(x);
        }

        public static bool LettersAndNumbersIsValid(string x)
        {
            Regex validar = new Regex(@"^[0-9a-zA-ZãáàéêíóõúçÃÁÀÉÊÍÓÕÚÇºª.,\s]+$");
            return validar.IsMatch(x);
        }

        public static bool LettersNumbersAndSomeSpecialCharIsValid(string x)
        {
            Regex validar = new Regex(@"^[0-9a-zA-ZãáàéêíóõúçÃÁÀÉÊÍÓÕÚÇºª.,()--?!'%:\s]+$");
            return validar.IsMatch(x);
        }

        #endregion

        // PropertyInfo/Values
        #region PropertyInfo/Values
        public static PropertyInfo PropertyValue(ValidationContext validationContext, string propertyName)
        {
            return validationContext.ObjectInstance.GetType().GetProperty(propertyName);
        }
        public static string PropertyValueToString(PropertyInfo value)
        {
            return value != null ? value.ToString() : string.Empty;
        }
        public static string FieldValueToString(object value)
        {
            return value != null ? value.ToString() : string.Empty;
        }

        #endregion

        // Validation Results
        #region Validation Results
        public static ValidationResult FieldIsRequired(ValidationContext validationContext)
        {
            return new ValidationResult($"{validationContext.DisplayName} is required");
        }
        public static ValidationResult PropertyValueEmpty(string value)
        {
            return new ValidationResult($"{value} is empty");
        }

        #endregion
    }
}
