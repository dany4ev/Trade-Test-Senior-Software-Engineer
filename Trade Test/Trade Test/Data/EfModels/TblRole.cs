using System.ComponentModel.DataAnnotations;

namespace Trade_Test.Data.EfModels
{
    public partial class TblRole
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(254)]
        public string Type { get; set; }
    }
}
