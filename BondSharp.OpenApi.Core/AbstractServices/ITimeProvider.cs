namespace BondSharp.OpenApi.Core.AbstractServices;
public interface ITimeProvider
{
    DateTimeOffset UtcNow();
}
