using System.ComponentModel.DataAnnotations;

namespace Trade_Test.Data.EfModels
{
    public class TblUser
    {
        public TblUser()
        {

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
    }
}
