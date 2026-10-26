using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;



namespace MusicPortal.Models
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    FirstName = "Наталка",
                    LastName = "Діденко",
                    Login = "Natali",
                    Email = "Natali@gmail.com",
                    Password = "123123",
                },

                new Users
                {
                    Id = 2,
                    FirstName = "Ангеліна",
                    LastName = "Степанюк",
                    Login = "Angel",
                    Email = "Angela1@gmail.com",
                    Password = "qwery",
                },

                new Users
                {
                    Id = 3,
                    FirstName = "Сергій",
                    LastName = "Степанюк",
                    Login = "Serg",
                    Email = "Serg888@gmail.com",
                    Password = "qwery123",
                },

                new Users
                {
                    Id = 4,
                    FirstName = "Дмитро",
                    LastName = "Шпак",
                    Login = "Dmytro",
                    Email = "ShpakDmytro@gmail.com",
                    Password = "888888",
                },

                new Users
                {
                    Id = 5,
                    FirstName = "София",
                    LastName = "Ковальчук",
                    Login = "Sofi",
                    Email = "Sofi@gmail.com",
                    Password = "zxcvb",
                }

            );

            modelBuilder.Entity<Genres>().HasData(
             new Genres { Id = 1, Genre = "Deep House" },
             new Genres { Id = 2, Genre = "Pop" },
             new Genres { Id = 3, Genre = "Rock" },
             new Genres { Id = 4, Genre = "Rap" }
         );

            modelBuilder.Entity<Singers>().HasData(
             new Singers { Id = 1, Singer = "Shakira", Image = "/image/shakira.jpg"},
             new Singers { Id = 2, Singer = "Adele", Image = "/image/adele.jpg" },
             new Singers { Id = 3, Singer = "Imagine Dragons", Image = "/image/imagine-dragons.jpg" },
             new Singers { Id = 4, Singer = "Eminem", Image = "/image/eminem.jpg" },
             new Singers { Id = 5, Singer = "The Weeknd", Image = "/image/the-weeknd.jpg" },
             new Singers { Id = 6, Singer = "Kimo Sounds", Image = "/image/kimo-sounds.jpg" }
          );

            modelBuilder.Entity<Song>().HasData(
             new Song { Id = 1, NameOfSong = "Dai Dai", Foto = "/picture/foto2.jpg",
                 AudioPath = "/music/Shakira Dai Dai (feat. Burna Boy).mp4",
                 SingersId = 1, GenreId = 2, UserId = 1, IsAccept = true },
             new Song
             {
                 Id = 2,
                 NameOfSong = "Waka Waka (Dj Dark Remix)",
                 Foto = "/picture/shakira-waka-waka-dj-dark-remix.webp.jpg",
                 AudioPath = "/music/Shakira - Waka Waka (Dj Dark Remix).mp3",
                 SingersId = 1,
                 GenreId = 2,
                 UserId = 3,
                 IsAccept = true
             },
             new Song { Id = 3, NameOfSong = "Someone Like You", Foto = "/picture/adele-someone-like-you.jpg", AudioPath = "/music/Adele - Someone Like You.mp3", SingersId = 2, GenreId = 2, UserId = 1, IsAccept = true },
             new Song { Id = 4, NameOfSong = "Rise Slow", Foto = "/picture/song1.jpg", AudioPath = "/music/Kimo Sounds Rise Slow.mp4", SingersId = 6, GenreId = 1, UserId = 1, IsAccept = true }
           );
        }


        public DbSet<Users> User => Set<Users>();
        public DbSet<Song> Messages => Set<Song>();
        public DbSet<Genres> Genres => Set<Genres>();
        public DbSet<Singers> Singers => Set<Singers>();

        public DbSet<Song> Songs => Set<Song>();

    }
}
