using System.ComponentModel.DataAnnotations;
using System;


namespace MusicPortal.Models
{
    public class Genres
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Будь ласка, введіть Жанр")]
        [Display(Name = "Жанр")]
        public required string Genre { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();

    }
}
