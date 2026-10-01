using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IRepositorySong
    {
        Task<IEnumerable<Song>> GetSongListAsync();
    }
}
