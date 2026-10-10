using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class GenreController(IRepositoryGenre repos) : Controller
    {
        public IActionResult Genre() => View();

        public async Task<IActionResult> Start()
        {
            var genres = await repos.GetGenreListAsync();
            return View(genres);
        }

        public async Task<IActionResult> Details(int id)
        {
            var genre = await repos.GetGenreAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
 
            return View(genre);
        }
    }
}
