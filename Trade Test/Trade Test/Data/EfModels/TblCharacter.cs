using System.ComponentModel.DataAnnotations;

namespace Trade_Test.Data.EfModels
{
    public class TblCharacter
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string? Name { get; set; }
        public int Vote { get; set; }

        public string? FileType { get; set; }
        public IEnumerable<byte>? FileData { get; set; }
    }
}
