using System.ComponentModel.DataAnnotations;

namespace Store.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Пуста комірка")]
        [StringLength(100, MinimumLength = 6, ErrorMessage ="Довжина паролю від 6 до 100 символів")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Required(ErrorMessage = "Пуста комірка")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Пуста комірка")]
        public string Name { get; set; }
    }
}
