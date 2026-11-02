using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;


namespace MusicPortal.Repository
{
    public class SongRepository(MusicContext context) : IRepositorySong
    {
        public async Task<IEnumerable<Song>> GetSongListAsync()
        {
            return await context.Songs
                .Where(p => !p.IsAccept)
                .Include(p => p.User)
                .Include(p => p.Singer)
                .Include(p => p.Genre)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetAcceptedSongsAsync()
        {
            return await context.Songs
                .Where(p => p.IsAccept)
                .Include(p => p.Singer)
                .Include(p => p.Genre)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Song>> GetSongsBySingerAsync(int singerId)
        {
            return await context.Songs
                .Where(p => p.SingersId == singerId)
                .Include(p => p.Singer)
                .Include(p => p.Genre)
                .AsNoTracking()
                .ToListAsync();
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

        public async Task DeleteSong(int id)
        {
            var st = await context.Songs.FindAsync(id);
            if (st != null)
            {
                context.Songs.Remove(st);
            }
        }

        public void Update(Song si)
        {
            context.Songs.Update(si);
        }
        public Task SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
