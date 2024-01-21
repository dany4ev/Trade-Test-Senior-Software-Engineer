using Trade_Test.Data.UnitOfWork;
using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Services {
    public class AdminService : IAdminService {

        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task AddUserAsync(User user) {
            await _unitOfWork.AdminRepository.AddUserAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserAsync(User user) {
            await _unitOfWork.AdminRepository.UpdateUserAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public List<User> GetUsers() {
            var result = _unitOfWork.AdminRepository.GetUsers();
            return result;
        }

        public User GetUser(int id) {
            var result = _unitOfWork.AdminRepository.GetUser(id);
            return result;
        }
    }
}
