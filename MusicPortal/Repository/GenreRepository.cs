using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public class GenreRepository(MusicContext context) : IRepositoryGenre
    {
        public async Task<IEnumerable<Genres>> GetGenreListAsync()
        {
            return await context.Genres.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByGenreAsync(int genreId)
        {
            return await context.Songs
                .Where(s => s.GenreId == genreId)
                .Include(s => s.Genre)
                .Include( s => s.Singer).AsNoTracking().ToListAsync();
        }
        public async Task<Genres?> GetGenreAsync(int id)
        {
            return await context.Genres.Include(g=> g.Songs).ThenInclude(s => s.Singer).FirstOrDefaultAsync(g => g.Id == id);
        }
     
        public async Task<bool> GenreExistsAsync(string name)
        {
            return await context.Genres.AnyAsync(g => g.Genre == name);
        }
        public async Task AddGenreAsync(Genres item)
        {
            await context.Genres.AddAsync(item);
        }

        public async Task DeleteGenre(Genres item)
        {
            context.Genres.Remove(item);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
