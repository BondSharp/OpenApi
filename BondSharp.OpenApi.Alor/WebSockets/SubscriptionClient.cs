namespace BondSharp.OpenApi.Alor.WebSockets;
internal class SubscriptionClient : BaseClient
{
    public SubscriptionClient(Settings settings) : base(settings)
    {
    }

    protected override string ProductionAddress => "wss://api.alor.ru/ws";

    protected override string DevelopmentAddress => "wss://apidev.alor.ru/ws";
}
