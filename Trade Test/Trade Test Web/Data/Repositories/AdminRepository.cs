using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

using Trade_Test_Web.Data.EfModels;
using Trade_Test_Web.Models.Enums;

namespace Trade_Test.Data.Repositories {
    public class AdminRepository : IAdminRepository {

        private readonly UserManager<User> _userManager;

        public AdminRepository(
            ApplicationDbContext dbContext,
            UserManager<User> userManager
            ) {

            _userManager = userManager;
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public async Task AddUserAsync(User userData) {

            if (await _userManager.FindByEmailAsync(userData.Email) == null) {

                User newUser = new() {
                    UserName = userData.UserName,
                    Email = userData.Email,
                    PhoneNumber = userData.PhoneNumber,
                    PasswordHash = userData.PasswordHash
                };

                await _userManager.CreateAsync(newUser);

                await _userManager.AddToRoleAsync(newUser, nameof(RoleType.Patron));
            }
        }

        public async Task UpdateUserAsync(User userData) {

            var savedUser = await DbContext.Users.FindAsync(userData.Id);

            if (savedUser != null) {
                savedUser = userData;
                DbContext.Users.Update(savedUser);
            }
        }

        public List<User> GetUsers() {

            var usersList = DbContext.Users.Select(s => new User {
                Id = s.Id,
                Email = s.Email,
                UserName = s.UserName,
                PhoneNumber = s.PhoneNumber
            }).ToList();

            return usersList;
        }

        public User GetUser(int id) {

            var savedCharacter = DbContext.Users.First(c => c.Id == id.ToString());

            User result = new();

            if (savedCharacter != null) {
                result.UserName = savedCharacter.UserName;
                result.Email = savedCharacter.Email;
                result.PhoneNumber = savedCharacter.PhoneNumber;
            }

            return result;
        }
    }
}
