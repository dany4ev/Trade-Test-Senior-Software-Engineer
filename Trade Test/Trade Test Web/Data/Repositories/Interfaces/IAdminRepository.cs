using Trade_Test.Models;

namespace Trade_Test.Data.Repositories.Interfaces
{
    public interface IAdminRepository {
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        List<User> GetUsers();
        User GetUser(int id);
    }
}
