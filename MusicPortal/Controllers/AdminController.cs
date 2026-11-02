using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;
using static System.Net.WebRequestMethods;

namespace MusicPortal.Controllers
{
    public class AdminController(IRepositorySinger reposi, IRepositoryUser userRepository, IRepositorySong songRepository,
        IRepositoryGenre genreRepository, IWebHostEnvironment appEnvironment) : Controller
    {
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Login") == "ADMIN";
        }


        public async Task<IActionResult> Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View();
        }

        public async Task<IActionResult> Genres()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var model = await genreRepository.GetGenreListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }



        public IActionResult CreateGenre() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre([Bind("Id,Genre")] Genres genres)
        {
            if (!ModelState.IsValid) return View(genres);

            await genreRepository.AddGenreAsync(genres);
            await genreRepository.SaveAsync();
            return RedirectToAction(nameof(Genres));
        }

        [HttpGet]
        public async Task<IActionResult> EditGenre(int? id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (id is null)
                return NotFound();

            var genre = await genreRepository.GetGenreAsync(id.Value);

            if (genre is null)
                return NotFound();


            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(int id, [Bind("Id,Genre")] Genres genres)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (id != genres.Id) return NotFound();

            if (!ModelState.IsValid) return View(genres);

            try
            {
                genreRepository.Update(genres);
                await genreRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await genreRepository.GetGenreAsync(genres.Id) == null)
                    return NotFound();

                throw;
            }
            return RedirectToAction(nameof(Genres));
        }

        public async Task<IActionResult> DeleteGenre(int? id)
        {
            if (id is null) return NotFound();

            var genre = await genreRepository.GetGenreAsync(id.Value);
            if (genre is null) return NotFound();

            return View(genre);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var genre = await genreRepository.GetGenreAsync(id);

            if (genre is null)
                return NotFound();

            await genreRepository.DeleteGenre(id);
            await genreRepository.SaveAsync();
            return RedirectToAction(nameof(Genres));
        }

        public async Task<IActionResult> Singers()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var model = await reposi.GetSingerListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditSinger(int? id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (id is null)
                return NotFound();

            var singer = await reposi.GetSingerAsync(id.Value);

            if (singer is null)
                return NotFound();


            return View(singer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSinger(int id, [Bind("Id,Singer, Image")] Singers singers, IFormFile? posterFile)
        {
            if (!ModelState.IsValid) return View(singers);

            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (posterFile is not null && posterFile.Length > 0)
            {
                var fileName = Path.GetFileName(posterFile.FileName);
                var relativePath = $"/image/{fileName}";
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "image", fileName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(fileStream);
                }
                singers.Image = relativePath;
            }

            try
            {
                reposi.Update(singers);
                await reposi.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await reposi.GetSingerAsync(singers.Id) == null)
                    return NotFound();

                throw;
            }
            return RedirectToAction(nameof(Singers));
        }

        public async Task<IActionResult> DeleteSinger(int? id)
        {
            if (id is null) return NotFound();

            var singer = await reposi.GetSingerAsync(id.Value);
            if (singer is null) return NotFound();

            return View(singer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSinger(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var singer = await reposi.GetSingerAsync(id);

            if (singer is null)
                return NotFound();

            await reposi.DeleteSinger(id);
            await reposi.SaveAsync();
            return RedirectToAction(nameof(Singers));
        }

        public IActionResult CreateSinger() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSinger(int id, [Bind("Id,Singer, Image")] Singers singers, IFormFile? posterFile)
        {

            if (posterFile is not null && posterFile.Length > 0)
            {

                var fileName = Path.GetFileName(posterFile.FileName);
                var relativePath = $"/image/{fileName}";
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "image", fileName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(fileStream);
                }
                singers.Image = relativePath;
            }
            if (!ModelState.IsValid) return View(singers);
            await reposi.AddSingerAsync(singers);
            await reposi.SaveAsync();
            return RedirectToAction(nameof(Singers));
        }

        public async Task<IActionResult> Songs()
        {
            var songs = await songRepository.GetAcceptedSongsAsync();
            return View(songs);
        }

        public async Task<IActionResult> DeleteSong(int? id)
        {
            if (id is null) return NotFound();

            var song = await songRepository.GetSongAsync(id.Value);
            if (song is null) return NotFound();

            return View(song);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSong(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var song = await songRepository.GetSongAsync(id);

            if (song is null)
                return NotFound();

            await songRepository.DeleteSong(id);
            await songRepository.SaveAsync();
            return RedirectToAction(nameof(Songs));
        }

        [HttpGet]
        public async Task<IActionResult> EditSong(int? id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (id is null)
                return NotFound();

            var song = await songRepository.GetSongAsync(id.Value);

            if (song is null)
                return NotFound();

            await LoadLists();
            return View(song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSong(
    int id,
    [Bind("Id,NameOfSong,SingersId,GenreId")] Song song, 
    IFormFile? posterFile,
    IFormFile? audioFile)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                await LoadLists();
                return View(song);
            }

            var oldSong = await songRepository.GetSongAsync(id);

            if (oldSong == null)
                return NotFound();

            oldSong.NameOfSong = song.NameOfSong;
            oldSong.SingersId = song.SingersId;
            oldSong.GenreId = song.GenreId;

            if (posterFile != null && posterFile.Length > 0)
            {
                var fileName = Path.GetFileName(posterFile.FileName);
                var relativePath = $"/image/{fileName}";
                var absolutePath = Path.Combine(
                    appEnvironment.WebRootPath, "image", fileName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(fileStream);
                }

                oldSong.Foto = relativePath;
            }

            if (audioFile != null && audioFile.Length > 0)
            {
                var audioName = Path.GetFileName(audioFile.FileName);
                var relativePath = $"/music/{audioName}";
                var absolutePath = Path.Combine(
                    appEnvironment.WebRootPath, "music", audioName);

                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(fileStream);
                }

                oldSong.AudioPath = relativePath;
            }

            await songRepository.SaveAsync();

            return RedirectToAction(nameof(Songs));
        }

        private async Task LoadLists()
        {
            var singers = await reposi.GetSingerListAsync();
            var genres = await genreRepository.GetGenreListAsync();

            ViewBag.Singers = new SelectList(
                singers, "Id", "Singer");

            ViewBag.Genres = new SelectList(
                genres, "Id", "Genre");
        }

        public async Task<IActionResult> ForAcceptUser()
        {
            if (HttpContext.Session.GetString("Login") != "ADMIN")
            {
                return RedirectToAction("Clients", "Account");
            }

            var users = await userRepository.GetUserListAsync();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id)
        {
            if (HttpContext.Session.GetString("Login") != "ADMIN")
            {
                return RedirectToAction("Clients", "Account");
            }

            var user = await userRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.isAccept = true;

            await userRepository.SaveAsync();

            return RedirectToAction("ForAcceptUser", "Admin");
        }

        public async Task<IActionResult> Users()
        {
            var users = await userRepository.GetAcceptedUserListAsync();
            return View(users);
        }

        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id is null) return NotFound();

            var user = await userRepository.GetUserAsync(id.Value);
            if (user is null) return NotFound();

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUser(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var user = await userRepository.GetUserAsync(id);

            if (user is null)
                return NotFound();

            await userRepository.DeleteUser(id);
            await userRepository.SaveAsync();
            return RedirectToAction(nameof(Users));
        }

    }
}
