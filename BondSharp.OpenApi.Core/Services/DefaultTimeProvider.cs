using BondSharp.OpenApi.Core.AbstractServices;

namespace BondSharp.OpenApi.Core.Services;
public class DefaultTimeProvider : ITimeProvider
{
    public DefaultTimeProvider()
    {
    }

    public DateTimeOffset UtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}
