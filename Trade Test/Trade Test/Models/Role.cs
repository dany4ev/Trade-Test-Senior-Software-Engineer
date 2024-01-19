using Trade_Test.Models.Enums;

namespace Trade_Test.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public RoleType Type {  get;set; }
    }
}
