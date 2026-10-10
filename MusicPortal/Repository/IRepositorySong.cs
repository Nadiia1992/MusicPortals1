using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IRepositorySong
    {
        Task<IEnumerable<Song>> GetSongListAsync();
        Task<IEnumerable<Song>> GetSongsBySingerAsync(int singerId);
        Task<Song?> GetSongAsync(int id);
        Task<bool> SongExistsAsync(string name);
        Task AddSongAsync(Song item);
        Task DeleteSongAsync(Song item);
        Task EditSong(Song item);
        Task SaveAsync();
        
    }
}
