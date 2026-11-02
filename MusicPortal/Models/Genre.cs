using System.ComponentModel.DataAnnotations;
using System;


namespace MusicPortal.Models
{
    public class Genres
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "GenreRequired")]
        [Display(Name = "Genre")]
        public required string Genre { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();

    }
}
