using System.Security.Cryptography;


namespace Trade_Test.Utilities.Extensions
{
    public static class SequentialGuidGenerator
    {
        private static int _sequenced = (int)DateTime.UtcNow.Ticks;
        private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();
        private static readonly byte[] _buffer = new byte[6];

        public static Guid Generate()
        {
            long ticks = DateTime.UtcNow.Ticks;
            int sequenceNum = Interlocked.Increment(ref _sequenced);

            lock (_buffer)
            {
                _random.GetBytes(_buffer);
                return new Guid(
                (int)(ticks >> 32), (short)(ticks >> 16), (short)ticks,
                (byte)(sequenceNum >> 8), (byte)sequenceNum,
                _buffer[0], _buffer[1], _buffer[2], _buffer[3], _buffer[4], _buffer[5]);
            }
        }
    }
}
