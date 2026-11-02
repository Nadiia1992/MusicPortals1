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
        Task DeleteSong(int id);
        void Update(Song si);
        Task SaveAsync();
        Task<IEnumerable<Song>> GetAcceptedSongsAsync();
    }
}
