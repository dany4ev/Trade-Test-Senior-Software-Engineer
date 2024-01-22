using Trade_Test.Models;

namespace Trade_Test.Services.Interfaces
{
    public interface IAdminService {
        void AddUser(User user);
        void UpdateUser(User user);
        List<User> GetUsers();
        User GetUser(Guid id);
    }
}
