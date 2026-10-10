using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicPortal.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Singers> singers, int singer, string? nameOfSong)
        {
            singers.Insert(0, new Singers { Singer = "Всі", Id = 0 });
            Singers = new SelectList(singers, "Id", "Name", singer);
            SelectedSinger = singer;
            SelectedNameOfSong = nameOfSong;
        }

        public SelectList Singers { get; }
        public int SelectedSinger { get; }
        public string? SelectedNameOfSong { get; }
    }
}
