using System.Reactive.Linq;
using System.Text.Json;
using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Alor.Common;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class SubscriptionClient : BaseClient
{
    protected override string ProductionAddress => "wss://api.alor.ru/ws";

    protected override string DevelopmentAddress => "wss://apidev.alor.ru/ws";

    private readonly IObservable<object> objects;//= Messages.Select(x => (object)x);
    private readonly SubscriptionCollection subscriber;
    private readonly TokenAuthorization authorization;

    public SubscriptionClient(Settings settings, SubscriptionCollection subscriber, TokenAuthorization authorization) : base(settings)
    {
        this.subscriber = subscriber;
        this.authorization = authorization;
        this.objects = Messages.Select(Parse).Publish().RefCount();
        this.Events = objects.OfType<IEvent>().Publish().RefCount();
        this.Responces = objects.OfType<EmptyResponce>().Publish().RefCount();
    }

    public IObservable<IEvent> Events { get; }

    private IObservable<EmptyResponce> Responces { get; }

    private object Parse(string json)
    {
        using var jsonDocument = JsonDocument.Parse(json);
        if (jsonDocument.RootElement.TryGetProperty("requestGuid", out var _))
        {
            var reponce = jsonDocument.Deserialize<EmptyResponce>();
            return reponce!;
        }

        var data = jsonDocument.RootElement.GetProperty("data");
        var guid = jsonDocument.RootElement.GetProperty("guid").GetGuid();
        var subscription = subscriber.FindRequest(guid) ?? throw new Exception($"Not Found Request  {guid}");
        var @event = subscription.GetEvent(data);
        return @event;
    }

    public async Task<TimeSpan> Ping()
    {
        var datatime = DateTime.Now;
        var ping = new PingRequest();
        await Send(ping);

        return DateTime.Now - datatime;
    }

    public async Task<EmptyResponce> Send(BaseRequest request)
    {
        var token = authorization.Token();
        request.Token = token.AccessToken;
        var json = JsonSerializer.Serialize(request, request.GetType());
        var reponce = Responces
          .Where(x => x.RequestGuid == request.Guid)
          .FirstAsync()
          .Timeout(TimeSpan.FromSeconds(3));
        Send(json);

        return await reponce;
    }
}
