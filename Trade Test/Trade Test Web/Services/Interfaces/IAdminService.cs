using Trade_Test.Models;

namespace Trade_Test.Services.Interfaces
{
    public interface IAdminService {
        Task<int> AddUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        List<User> GetUsers();
        User GetUser(int id);
    }
}
