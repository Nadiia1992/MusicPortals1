using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using MusicPortal.Repository;
using MusicPortal.Services;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class AccountController(PasswordHasher passwordHasher, IRepositoryUser re, IRepositorySong repositorySong) : Controller
    {
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (!ModelState.IsValid) return View(logon);


            var user = await re.GetUserByLoginAsync(logon.Login);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Невірний логін або пароль!");
                return View(logon);
            }

            if (!passwordHasher.VerifyPassword(logon.Password, user.Salt, user.Password))

            {
                ModelState.AddModelError(string.Empty, "Невірний логін або пароль!");
                return View(logon);
            }

            if(user.isAccept == false)
            {
                ModelState.AddModelError(string.Empty, "Ваш обліковий запис ще не підтверджено!");
                return View(logon);
            }

            HttpContext.Session.SetString("Login", user.Login!);
            HttpContext.Session.SetString("FirstName", user.FirstName!);
            HttpContext.Session.SetString("LastName", user.LastName!);

            if (user.Login == "ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Clients", "Account");
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (!ModelState.IsValid) return View(reg);

            if (await re.UserExistsAsync(reg.Login))
            {
                ModelState.AddModelError(string.Empty, "Користувач з таким логіном вже існує!");
                return View(reg);
            }


            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            string salt = Convert.ToHexString(saltBytes);

            string hash = passwordHasher.HashPassword(reg.Password, salt);



            var user = new Users
            {
                FirstName = reg.FirstName,
                LastName = reg.LastName,
                Login = reg.Login,
                Salt = salt,
                Password = hash,
                isAccept = reg.Login == "ADMIN"
            };

            await re.AddUserAsync(user);
            await re.SaveAsync();

            return RedirectToAction(nameof(AcceptUser));
        }


        public async Task<IActionResult> AcceptUser()
        {
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Clients()
        {
            var songes = await repositorySong.GetAcceptedSongsAsync();
            return View(songes);
        }

        public IActionResult Admin()
        {
            if (HttpContext.Session.GetString("Login") != "ADMIN")
            {
                return RedirectToAction("Clients", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptUser(int id)
        {
            if (HttpContext.Session.GetString("Login") != "ADMIN")
            {
                return RedirectToAction("Clients", "Account");
            }

            var user = await re.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.isAccept = true;

            await re.SaveAsync();

            return RedirectToAction("AcceptUser", "Admin");
        }
    }
}
