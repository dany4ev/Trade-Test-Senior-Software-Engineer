
namespace Trade_Test.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Vote {  get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? ModifiedDateTime { get; set; }
        //public string? FileType { get; set; }
        //public IEnumerable<byte>? FileData { get; set; }
    }
}
