using System.ComponentModel.DataAnnotations;

namespace Store.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Пуста комірка")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" є обов'язковим")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } // Опція "Запам'ятати мене"
    }
}
