using System.ComponentModel.DataAnnotations;

namespace TaskManagerInAspNet.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "required.error")]
        [EmailAddress(ErrorMessage = "email.error")]
        public string Email { get; set; }
        [Required(ErrorMessage = "required.error")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
