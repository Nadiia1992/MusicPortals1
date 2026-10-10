using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;


namespace MusicPortal.Repository
{
    public class SongRepository(MusicContext context) : IRepositorySong
    {
        public async Task<IEnumerable<Song>> GetSongListAsync()
        {
            return await context.Songs.Include(p => p.User).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsBySingerAsync(int singerId)
        {
            return await context.Songs.Where(p => p.SingersId == singerId).AsNoTracking().ToListAsync();
        }

        public async Task<Song?> GetSongAsync(int id)
        {
            return await context.Songs.FindAsync(id);
        }

        public async Task<bool> SongExistsAsync(string name)
        {
            return await context.Songs.AnyAsync(p => p.NameOfSong == name);
        }

        public async Task AddSongAsync(Song item)
        {
            await context.Songs.AddAsync(item);
        }

        public async Task DeleteSongAsync(Song item)
        {
            context.Songs.Remove(item);
        }

        public async Task EditSong(Song item)
        {
            context.Songs.Update(item);
        }
        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
