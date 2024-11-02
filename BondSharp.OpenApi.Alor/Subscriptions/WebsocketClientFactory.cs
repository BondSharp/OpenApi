using BondSharp.OpenApi.Alor;
using Websocket.Client;

namespace AlorClient;

internal class WebSocketClientFactory
{
    private const string developmentAddress = "wss://apidev.alor.ru/ws";
    private const string productionAddress = "wss://api.alor.ru/ws";

    private readonly Settings settings;

    public WebSocketClientFactory(Settings settings)
    {
        this.settings = settings;
    }

    public IWebsocketClient Factory()
    {
        var uri = new Uri(settings.IsProduction ? productionAddress : developmentAddress);
        var webSocketClient = new WebsocketClient(uri);
        webSocketClient.ReconnectTimeout = settings.ReconnectTimeout;
        webSocketClient.ErrorReconnectTimeout = settings.ErrorReconnectTimeout;      
        return webSocketClient;
    }
}
