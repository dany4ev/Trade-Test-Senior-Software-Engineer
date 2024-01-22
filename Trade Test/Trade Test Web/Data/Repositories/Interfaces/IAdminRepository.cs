using Trade_Test.Models;

namespace Trade_Test.Data.Repositories.Interfaces
{
    public interface IAdminRepository {
        void AddUser(User user);
        void UpdateUser(User user);
        List<User> GetUsers();
        User GetUser(int id);
    }
}
