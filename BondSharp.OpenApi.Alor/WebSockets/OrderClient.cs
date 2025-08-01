namespace BondSharp.OpenApi.Alor.WebSockets;
internal class OrderClient : BaseClient
{
    public OrderClient(Settings settings) : base(settings)
    {
    }

    protected override string ProductionAddress => "wss://api.alor.ru/cws";

    protected override string DevelopmentAddress => "wss://apidev.alor.ru/cws";
}
