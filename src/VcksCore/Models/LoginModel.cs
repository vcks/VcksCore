
using System.ComponentModel.DataAnnotations;

namespace VcksCore.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Электронная почта")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}