using Microsoft.EntityFrameworkCore;

using Trade_Test.Data.EfModels;
using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

using Trade_Test_Web.Data.EfModels;

namespace Trade_Test.Data.Repositories {
    public class AdminRepository : IAdminRepository {
        public AdminRepository(ApplicationDbContext dbContext) {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public async Task<int> AddUserAsync(User userData) {

            User newUser = new() {
                UserName = userData.UserName,
                Email = userData.Email,
                PhoneNumber = userData.PhoneNumber
            };

            DbContext.Add(newUser);

            var result = await DbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> UpdateUserAsync(User userData) {

            var savedUser = await DbContext.Users.FindAsync(userData.Id);

            if(savedUser != null) {
                savedUser = userData;
                DbContext.Users.Update(savedUser);
            }

            var result = await DbContext.SaveChangesAsync();

            return result;
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
