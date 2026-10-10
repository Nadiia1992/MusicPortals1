using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;


namespace MusicPortal.Controllers
{
    public class SongController(IRepositorySong repositorySong) : Controller
    {
        public async Task<IActionResult> Index(string songName, int singer = 0, int page = 1, 
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;

            IQueryable<Song> songs = await repositorySong.GetSongListAsync();

            if (singer != 0)
            {
                songs = songs.Where(s => s.Singer.Id == singer);
            }
        
            var songs = await repositorySong.GetSongListAsync();

            if (!string.IsNullOrEmpty(songName))
            {
                songs = songs.Where(s => s.NameOfSong!.Contains(songName, StringComparison.OrdinalIgnoreCase));
            }

            songs = sortOrder switch
            {
                SortState.SingerAsc => songs.OrderBy(s => s.Singer!.Singer),
                SortState.SingerDesc => songs.OrderByDescending(s => s.Singer!.Singer),
                SortState.GenreAsc => songs.OrderBy(s => s.Genre!.Genre),
                SortState.GenreDesc => songs.OrderByDescending(s => s.Genre!.Name),
                SortState.NameOfSongAsc => songs.OrderBy(s => s.NameOfSong),
                SortState.NameOfSongDesc => songs.OrderByDescending(s => s.NameOfSong),
                _ => songs.OrderBy(s => s.NameOfSong)
            };
        }
           
           
            return View(songs);
        }
    }
}
