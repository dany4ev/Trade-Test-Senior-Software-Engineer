using Trade_Test.Models.Enums;

namespace Trade_Test.Models
{
    public class PasswordOptions
    {

        public byte[] HashedPassword { get; set; }


        public byte[] Salt { get; set; }


        public HashStrategyKind HashStrategy { get; set; }

        public PasswordOptions()
        {
                
        }

        public PasswordOptions(string hashedPassword, string salt, HashStrategyKind hashStrategy)
        {
            HashedPassword = Convert.FromBase64String(hashedPassword);
            Salt = Convert.FromBase64String(salt);
            HashStrategy = hashStrategy;
        }
    }
}
