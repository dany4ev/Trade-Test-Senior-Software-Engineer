namespace Trade_Test.Models
{
    public class ErrorMessage
    {
        public int UniqueCode { get; set; }

        public int StatusCode { get; set; }
       
        public string FriendlyMessage { get; set; }

        public bool IsCurrentOrganizationBlocked { get; set; }

        public string StackTrace { get; set; }
    }
}
