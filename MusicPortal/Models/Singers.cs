using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Singers
    {
        public int Id { get; set; }

  
        [Display(Name = "Виконавець")]
        public string? Singer { get; set; }

        public string ? Image { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();

    }
}
