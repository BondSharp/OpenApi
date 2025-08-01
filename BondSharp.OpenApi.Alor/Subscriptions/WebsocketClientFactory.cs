using System.Net.WebSockets;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;

internal class WebSocketClientFactory
{
    private const string developmentAddress = "wss://apidev.alor.ru/ws";
    private const string productionAddress = "wss://api.alor.ru/ws";

    private const string developmentAddressOrders = "wss://apidev.alor.ru/ws";
    private const string productionAddressOrders = "wss://api.alor.ru/ws";

    private readonly Settings settings;

    public WebSocketClientFactory(Settings settings)
    {
        this.settings = settings;
    }

    public IWebsocketClient Factory()
    {
        var uri = new Uri(settings.IsProduction ? productionAddress : developmentAddress);
        var webSocketClient = new WebsocketClient(uri, () =>
        {
            var result = new ClientWebSocket();
            result!.Options.DangerousDeflateOptions = new WebSocketDeflateOptions
            {
                ClientMaxWindowBits = 15,
                ServerMaxWindowBits = 15,
                ClientContextTakeover = false,
                ServerContextTakeover = false,
            };

            return result;
        });

        webSocketClient.ReconnectTimeout = settings.ReconnectTimeout;
        webSocketClient.ErrorReconnectTimeout = settings.ErrorReconnectTimeout;
        webSocketClient.Start();
        return webSocketClient;
    }
}
