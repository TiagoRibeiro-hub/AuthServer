using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportDataAuth.Shared.AuthRequests
{
    public class RegisterRequest
    {
        [EmailValidaton(true)]
        public string Email { get; set; }

        [UserNameValidation(true)]
        public string UserName { get; set; }

        [PhoneNumberValidation(false)]
        public string PhoneNumber { get; set; }

        [PassWordValidation(true)]
        public string Password { get; set; }

        [StringCompareToValidation("Password")]
        public string ConfirmPassword { get; set; }


    }
}
