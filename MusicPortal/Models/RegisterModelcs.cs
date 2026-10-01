using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Введіть ім'я")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Довжина має бути від 2 до 20 символів.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Введіть прізвище")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Довжина має бути від 2 до 20 символів.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Введіть логін")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим для заповнення.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Некоректна адреса.")]
        [Display(Name = "Електронна пошта")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}
