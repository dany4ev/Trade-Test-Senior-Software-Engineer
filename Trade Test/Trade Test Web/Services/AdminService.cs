using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Services {
    public class AdminService : IAdminService {

        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository) {
            _adminRepository = adminRepository;
        }

        public Task<int> AddUserAsync(User user) {
            var result = _adminRepository.AddUserAsync(user);

            return result;
        }

        public Task<int> UpdateUserAsync(User user) {
            var result = _adminRepository.UpdateUserAsync(user);

            return result;
        }

        public List<User> GetUsers() {

            return _adminRepository.GetUsers();
        }
    }
}
