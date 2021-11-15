using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared.AuthRequests
{
    public class RoleRequests
    {
        [JustLettersValidationAttrribute(true)]
        [StringSizeValidation(5, 20)]
        public string Role { get; set; }
    }
}
