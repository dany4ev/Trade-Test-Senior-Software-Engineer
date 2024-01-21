using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

using Trade_Test_Web.Data.EfModels;

namespace Trade_Test.Data.Repositories {
    public class AdminRepository : IAdminRepository {

        private readonly ApplicationDbContext DbContext;

        public AdminRepository(
            ApplicationDbContext dbContext
            ) {
            DbContext = dbContext;
        }

        public async Task AddUserAsync(User userData) {

            try {
                User newUser = new() {
                    UserName = userData.UserName,
                    Email = userData.Email,
                    PhoneNumber = userData.PhoneNumber,
                    PasswordHash = userData.PasswordHash
                };

                var identityUserData = await DbContext.Users.FindAsync(userData.Email);

                if (identityUserData == null) {

                    DbContext.Users.Add(newUser);
                }
            }
            catch (Exception ex) {

                throw;
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
