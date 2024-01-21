using Microsoft.AspNetCore.Identity;

namespace Trade_Test.Models
{
    public class User : IdentityUser {
        public string UserName { get; set; }
        public string? Email { get; set; }
    }
}
