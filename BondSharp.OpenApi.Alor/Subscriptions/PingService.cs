namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class PingService
{
    private Byte[] randomBytes = new byte[8];

    private static Random random = new Random();
    public PingService()
    {
        lock (random)
        {
            random.NextBytes(randomBytes);
        }

    }
    public Guid CreateGuidRequest()
    {
        var ticks = DateTime.UtcNow.Ticks;
        var bytes = new byte[16];
        randomBytes.CopyTo(bytes, 0);
        BitConverter.GetBytes(ticks).CopyTo(bytes, 8);

        return new Guid(bytes);
    }

    public bool TryParse(Guid guid, out TimeSpan delay)
    {
        var bytes = guid.ToByteArray();
        var ticks = BitConverter.ToInt64(bytes, 8);
        Span<byte> aRandomBytes = new Span<byte>(bytes, 0, 8);
        if (aRandomBytes.SequenceEqual(randomBytes))
        {
            var now = DateTime.UtcNow;
            if (now.Ticks >= ticks && now.AddMinutes(-1).Ticks <= ticks)
            {
                var timeSend = new DateTime(ticks, DateTimeKind.Utc);
                delay = now - timeSend;
                return true;
            }
        }
        delay = TimeSpan.Zero;
        return false;

    }
}
