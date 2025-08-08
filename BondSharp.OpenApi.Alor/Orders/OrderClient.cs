using System.Reactive.Linq;
using System.Text.Json;
using BondSharp.OpenApi.Alor.Common;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Alor.Orders.Data;

namespace BondSharp.OpenApi.Alor.Orders;
internal class OrderClient : BaseClient
{
    public OrderClient(Settings settings) : base(settings)
    {
        Responces = Messages.Select(Parse);
    }

    protected override string ProductionAddress => "wss://api.alor.ru/cws";

    protected override string DevelopmentAddress => "wss://apidev.alor.ru/cws";

    public IObservable<EmptyResponce> Responces { get; }


    internal void Send(BaseRequest request)
    {
        var json = JsonSerializer.Serialize(request, request.GetType());
        Send(json);
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
}
