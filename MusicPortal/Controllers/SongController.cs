using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class SongController(IRepositorySong repositorySong, IRepositorySinger repositorySinger,
    IRepositoryGenre repositoryGenre, IRepositoryUser repositoryUser, IWebHostEnvironment appEnvironment) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var songs = await repositorySong.GetAcceptedSongsAsync();
            return View(songs);
        }

        public async Task<IActionResult> Clients()
        {
            var songs = await repositorySong.GetAcceptedSongsAsync();
            return View(songs);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLists();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Song song, IFormFile? posterFile, IFormFile? audioFile)
        { 
            if (posterFile == null || posterFile.Length == 0)
            {
                ModelState.AddModelError(nameof(song.Foto),
                    "Завантажте обкладинку пісні.");
            }

            if (audioFile == null || audioFile.Length == 0)
            {
                ModelState.AddModelError(nameof(song.AudioPath),"Завантажте аудіофайл.");
            }

            var login = HttpContext.Session.GetString("Login");

            if (string.IsNullOrEmpty(login))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await repositoryUser.GetUserByLoginAsync(login);

            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            song.UserId = user.Id;
            if (HttpContext.Session.GetString("Login") == "ADMIN")
            {
                song.IsAccept = true;
            }
            else
            {
                song.IsAccept = false;
            }

            if (!ModelState.IsValid)
            {
                await LoadLists();
                return View(song);
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads");

            Directory.CreateDirectory(uploadsFolder);

            if (song.SingersId <= 0)
            {
                ModelState.AddModelError(nameof(song.SingersId),
                    "Оберіть виконавця.");

                await LoadLists();
                return View(song);
            }

            if (posterFile is not null && posterFile.Length > 0)
            {
                var fileName = Path.GetFileName(posterFile.FileName);
                var relativePath = $"/image/{fileName}";
                var absolutePath = Path.Combine(
                    appEnvironment.WebRootPath, "image", fileName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(fileStream);
                }
                song.Foto = relativePath;
            }

            if (audioFile != null && audioFile.Length > 0)
            {
                var audioName = Path.GetFileName(audioFile.FileName);

                var relativePath = $"/music/{audioName}";
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "music", audioName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fileStream);
                }
                song.AudioPath = relativePath;
                
            }
            await repositorySong.AddSongAsync(song);
            await repositorySong.SaveAsync();

            if (song.IsAccept == false)
            {
                return RedirectToAction("AcceptSong", "Song");
            }

            return RedirectToAction("Index", "Song");

        }

        private async Task LoadLists()
        {
            var singers = await repositorySinger.GetSingerListAsync();
            var genres = await repositoryGenre.GetGenreListAsync();

            ViewBag.Singers = new SelectList(
                singers, "Id", "Singer");

            ViewBag.Genres = new SelectList(
                genres, "Id", "Genre");
        }

        public async Task<IActionResult> forAccept()
        {
            if (HttpContext.Session.GetString("Login") != "ADMIN")
            {
                return RedirectToAction("Clients", "Account");
            }

            var songs = await repositorySong.GetSongListAsync();

            return View(songs);
        }

        public async Task<IActionResult> AcceptSong()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id)
        {
            if (HttpContext.Session.GetString("Login") != "ADMIN")
            {
                return RedirectToAction("Clients", "Account");
            }

            var song = await repositorySong.GetSongAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            song.IsAccept = true;

            await repositorySong.SaveAsync();

            return RedirectToAction("forAccept", "Song");
        }
    }
}
