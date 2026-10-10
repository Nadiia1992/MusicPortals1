using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;


namespace MusicPortal.Repository
{
    public interface IRepositoryGenre
    {
        Task<IEnumerable<Genres>> GetGenreListAsync();
        Task<IEnumerable<Song>> GetSongsByGenreAsync(int genreId);
        Task<Genres?> GetGenreAsync(int id);
        Task<bool> GenreExistsAsync(string name);
        Task AddGenreAsync(Genres item);
        Task DeleteGenre(Genres item);
        Task SaveAsync();
        
    }
}
