using System.Net.WebSockets;
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
        var webSocketClient = new WebsocketClient(uri, () =>
        {
            var result = new System.Net.WebSockets.ClientWebSocket();
            result!.Options.DangerousDeflateOptions = new WebSocketDeflateOptions
            {
                ClientMaxWindowBits = 15,           // Max window size for client compression (15 bits is the maximum allowed)
                ServerMaxWindowBits = 15,           // Max window size for server compression
                ClientContextTakeover = true,       // Enable client-side context takeover (preserve compression context between messages)
                ServerContextTakeover = true        // Enable server-side context takeover
            }; 
      
            return result;
        });
        
        webSocketClient.ReconnectTimeout = settings.ReconnectTimeout;
        webSocketClient.ErrorReconnectTimeout = settings.ErrorReconnectTimeout;      
        return webSocketClient;
    }
}
