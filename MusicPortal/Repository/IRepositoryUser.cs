using MusicPortal.Models;


namespace MusicPortal.Repository
{
    public interface IRepositoryUser
    {
        Task<IEnumerable<Users>> GetUserListAsync();
        Task<IEnumerable<Users>> GetAcceptedUserListAsync();
        Task<Users?> GetUserAsync(int id);
        Task<Users?> GetUserByLoginAsync(string logon); 
        Task<bool> UserExistsAsync(string login);
        Task AddUserAsync(Users item);
        Task DeleteUser(int id);
        Task SaveAsync();
    }
}
