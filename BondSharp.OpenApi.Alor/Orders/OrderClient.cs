using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text.Json;
using BondSharp.OpenApi.Alor.Common;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Alor.Orders.Data;

namespace BondSharp.OpenApi.Alor.Orders;
internal class OrderClient : BaseClient
{
    public OrderClient(Settings settings) : base(settings)
    {
        Responces = Messages.Select(Parse).Publish().RefCount();
    }

    protected override string ProductionAddress => "wss://api.alor.ru/cws";

    protected override string DevelopmentAddress => "wss://apidev.alor.ru/cws";

    private IObservable<EmptyResponce> Responces { get; }


    public async Task<EmptyResponce> Send(BaseRequest request)
    {
        var json = JsonSerializer.Serialize(request, request.GetType());
        var result = Responces.FirstAsync(responce => responce.RequestGuid == request.Guid)
            .Timeout(TimeSpan.FromSeconds(3)).ToTask();
        Send(json);

        var reponce = await result;

        return reponce;
    }

    private EmptyResponce Parse(string json)
    {
        var reponce = JsonDocument.Parse(json);
        if (reponce.RootElement.TryGetProperty("orderNumber", out _))
        {
            return reponce.Deserialize<OrderNumberResponse>()!;
        }

        return reponce.Deserialize<EmptyResponce>()!;

    }

    public async Task<TimeSpan> Ping()
    {
        var datatime = DateTime.Now;
        var ping = new PingRequest()
        {
            OperationCode = "ping"
        };

        await Send(ping);

        return DateTime.Now - datatime;
    }
}
