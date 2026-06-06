using System.ComponentModel.DataAnnotations;

namespace TaskManagerInAspNet.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Debe completar el campo: {0}")]
        [EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe completar el campo: {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
