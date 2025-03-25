using System.ComponentModel.DataAnnotations;
namespace Store.Web.Models
{
    public class ChangeProfileViewModel
    {
        [Required(ErrorMessage = "Пуста комірка")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введіть новий пароль!")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Довжина паролю від 6 до 30 символів")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        public string? cellPhone { get; set; }

    }
}
