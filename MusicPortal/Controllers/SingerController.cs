using Microsoft.AspNetCore.Mvc;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class SingerController(IRepositorySinger reposi) : Controller
    {
        public IActionResult Genre() => View();

        public async Task<IActionResult> Show()
        {
            var singers = await reposi.GetSingerListAsync();
            return View(singers);
        }

        public async Task<IActionResult> Info(int id)
        {
            var singer = await reposi.GetSingerAsync(id);
            if (singer == null)
            {
                return NotFound();
            }

            var songs = await reposi.GetSongsBySingerAsync(id);
            singer.Songs = songs.ToList();

            return View(singer);
        }
    }
}

