
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IRepositoryGenre
    {
        Task<IEnumerable<Genres>> GetGenreListAsync();
        Task<IEnumerable<Song>> GetSongsByGenreAsync(int genreId);
        Task<Genres?> GetGenreAsync(int id);
        Task<bool> GenreExistsAsync(string name);
        Task<bool> HasSongsAsync(int genreId);
        Task AddGenreAsync(Genres item);
        Task DeleteGenre(int id);
        Task SaveAsync();
        void Update(Genres ge);
    }
}
