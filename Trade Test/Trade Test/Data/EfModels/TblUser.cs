using System.ComponentModel.DataAnnotations;

namespace Trade_Test.Data.EfModels
{
    public class TblUser
    {
        public TblUser()
        {
            TblRoles = new HashSet<TblRole>();
        }


        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Phone { get; set; }

        public bool IsAdmin { get; set; }

        public string? Country { get; set; }

        
        [StringLength(200)]
        public string PasswordHash { get; set; }

        [StringLength(200)]
        public DateTimeOffset? PasswordLastChangedDate { get; set; }


        public virtual ICollection<TblRole> TblRoles { get; set; }
    }
}
