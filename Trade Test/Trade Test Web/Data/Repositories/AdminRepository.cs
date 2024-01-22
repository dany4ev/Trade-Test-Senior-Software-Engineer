using Microsoft.AspNetCore.Identity;

using Trade_Test.Data.Repositories.Interfaces;
using Trade_Test.Models;

using Trade_Test_Web.Data.EfModels;
using Trade_Test_Web.Models.Enums;

namespace Trade_Test.Data.Repositories {
    public class AdminRepository : IAdminRepository {

        private readonly ApplicationDbContext DbContext;

        public AdminRepository(
            ApplicationDbContext dbContext
            ) {
            DbContext = dbContext;
        }

        public void AddUser(User userData) {

            try {
                var identityUserData = DbContext.Users.Find(userData.Email);
                var role = DbContext.Roles.FirstOrDefault(f => f.Name.Equals(nameof(RoleType.Admin)));

                if (identityUserData == null) {

                    DbContext.Users.Add(new() {
                        Id = userData.Id,
                        UserName = userData.UserName,
                        Email = userData.Email,
                        PhoneNumber = userData.PhoneNumber,
                        PasswordHash = userData.PasswordHash
                    });

                    if(role != null) {

                        DbContext.UserRoles.Add(new IdentityUserRole<string>() {
                            RoleId = role.Id,
                            UserId = userData.Id
                        });
                    }

                    DbContext.SaveChanges();
                }
            }
            catch (Exception ex) {

                throw;
            }
        }

        public void UpdateUser(User userData) {

            var savedUser =  DbContext.Users.Find(userData.Id);

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
