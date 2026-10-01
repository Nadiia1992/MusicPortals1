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
    }
}
