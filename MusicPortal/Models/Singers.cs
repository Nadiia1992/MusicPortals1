using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Singers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "SingerRequired")]
        [Display(Name = "Виконавець")]
        public string? Singer { get; set; }

        [Display(Name = "Photo")]
        public string ? Image { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();

    }
}
