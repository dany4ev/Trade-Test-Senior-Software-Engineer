﻿using Trade_Test.Data.UnitOfWork;
using Trade_Test.Models;
using Trade_Test.Services.Interfaces;

namespace Trade_Test.Services {
    public class AdminService : IAdminService {

        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public Task<int> AddUserAsync(User user) {

            var result = _unitOfWork.AdminRepository.AddUserAsync(user);
            return result;
        }

        public Task<int> UpdateUserAsync(User user) {
            
            var result = _unitOfWork.AdminRepository.UpdateUserAsync(user);
            return result;
        }

        public List<User> GetUsers() {

            var result =  _unitOfWork.AdminRepository.GetUsers();
            return result;
        }

        public User GetUser(int id) {
            var result = _unitOfWork.AdminRepository.GetUser(id);
            return result;
        }
    }
}
