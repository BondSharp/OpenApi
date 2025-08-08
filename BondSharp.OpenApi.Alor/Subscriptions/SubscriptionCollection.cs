using BondSharp.OpenApi.Alor.Subscriptions.Requests;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class SubscriptionCollection()
{
    private readonly Dictionary<Guid, SubscriptionRequest> requests = new Dictionary<Guid, SubscriptionRequest>();

    public SubscriptionRequest? FindRequest(Guid guid)
    {
        return requests.GetValueOrDefault(guid);
    }

    public bool ContainsGuid(Guid guid)
    {
        return requests.ContainsKey(guid);
    }

    public void Add(SubscriptionRequest request)
    {
        if (requests.ContainsKey(request.Guid))
        {
            throw new ArgumentException($"Duplicate {nameof(request.Guid)} of {nameof(request)}");
        }
        requests.Add(request.Guid, request);
    }

    public IEnumerable<SubscriptionRequest> All() => requests.Values;

    public int Count() => requests.Count();
}
