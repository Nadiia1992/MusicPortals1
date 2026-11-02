using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "LoginRequired")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}