using Trade_Test.Models;

namespace Trade_Test.Data.Repositories.Interfaces
{
    public interface IAdminRepository {
        Task<int> AddUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        List<User> GetUsers();
    }
}
