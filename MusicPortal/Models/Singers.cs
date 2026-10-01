using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Singers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим для заповнення")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Довжина має бути від 5 до 500 символів.")]
        [Display(Name = "Виконавець")]
        public string? Singer { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();

    }
}
