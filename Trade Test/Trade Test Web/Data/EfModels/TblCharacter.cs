using System.ComponentModel.DataAnnotations;

namespace Trade_Test.Data.EfModels
{
    public class TblCharacter
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }
        public int? Vote { get; set; }

        [MaxLength(100)]
        public string? FileType { get; set; }

        [MaxLength]
        public byte[]? FileData { get; set; }

        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? ModifiedDateTime { get; set; }
    }
}
