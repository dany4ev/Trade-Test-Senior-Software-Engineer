using Trade_Test.Models;

namespace Trade_Test.Services.Interfaces
{
    public interface IAdminService {
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        List<User> GetUsers();
        User GetUser(int id);
    }
}
