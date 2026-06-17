using System.ComponentModel.DataAnnotations;

namespace TaskManagerInAspNet.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "required.error")]
        [EmailAddress(ErrorMessage = "email.error")]
        public string Email { get; set; }
        [Required(ErrorMessage = "required.error")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "stay.logged.in")]
        public bool RememberMe { get; set; }
    }
}
