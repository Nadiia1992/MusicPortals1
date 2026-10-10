using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using MusicPortal.Models;
using System.Text;


namespace MusicPortal.Repository
{
    public class UserRepository(MusicContext context) : IRepositoryUser
    {

        public async Task<IEnumerable<Users>> GetUserListAsync()
        {
            return await context.User.AsNoTracking().ToListAsync();
        }

        public async Task<Users?> GetUserAsync(int id)
        {
            return await context.User.FindAsync(id);
        }
        public async Task<Users?> GetUserByLoginAsync(string logon)
        {
            return await context.User.FirstOrDefaultAsync(a => a.Login == logon);
        }
        public async Task<bool> UserExistsAsync(string login)
        {
            return await context.User.AnyAsync(u => u.Login == login);
        }
        public async Task AddUserAsync(Users item)
        {
            await context.User.AddAsync(item);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
