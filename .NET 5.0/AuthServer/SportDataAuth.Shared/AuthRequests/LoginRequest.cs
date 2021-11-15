using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared.AuthRequests
{
    public class LoginRequest
    {
        [EmailValidaton(true)]
        public string Email { get; set; }

        [PassWordValidation(true)]
        public string Password { get; set; }


    }
}
