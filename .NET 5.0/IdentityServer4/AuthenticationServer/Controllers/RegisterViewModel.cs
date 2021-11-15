using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.Controllers
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Don't match")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}