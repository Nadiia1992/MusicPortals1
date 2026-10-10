using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public class SingerRepository(MusicContext context) : IRepositorySinger
    {
        public async Task<IEnumerable<Singers>> GetSingerListAsync()
        {
            return await context.Singers.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Song>> GetSongsBySingerAsync(int singerId)
        {
            return await context.Songs
               .Where(s => s.SingersId == singerId)
               .Include(s => s.Genre)
               .Include(s => s.Singer).AsNoTracking().ToListAsync();
        }
        public async Task<Singers?> GetSingerAsync(int id)
        {
            return await context.Singers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<bool> SingerExistsAsync(string name)
        {
            return await context.Singers.AnyAsync(s => s.Singer == name);
        }
        public async Task AddSingerAsync(Singers item)
        {
            context.Singers.Add(item);
            await SaveAsync();
        }
        public async Task DeleteSinger(Singers item)
        {
            context.Singers.Remove(item);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
