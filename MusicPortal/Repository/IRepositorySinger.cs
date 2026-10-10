using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IRepositorySinger
    {
        Task<IEnumerable<Singers>> GetSingerListAsync();
        Task<IEnumerable<Song>> GetSongsBySingerAsync(int singerId);
        Task<Singers?> GetSingerAsync(int id);
        Task<bool> SingerExistsAsync(string name);
        Task AddSingerAsync(Singers item);
        Task DeleteSinger(Singers item);
        Task SaveAsync();
      
    } 
}
