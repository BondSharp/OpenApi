using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text.Json;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.WebSockets;
internal abstract class BaseClient : IDisposable
{
    private IWebsocketClient websocket;

    protected abstract string ProductionAddress { get; }
    protected abstract string DevelopmentAddress { get; }

    public BaseClient(Settings settings)
    {
        websocket = GetWebsocket(settings);
    }
    public IObservable<string> Messages => websocket.MessageReceived.Select(x => x.Text!);

    public void Send(object request)
    {
        var json = JsonSerializer.Serialize(request, request.GetType());
        websocket.Send(json);
    }

    private IWebsocketClient GetWebsocket(Settings settings)
    {
        var uri = new Uri(settings.IsProduction ? ProductionAddress : DevelopmentAddress);
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

    public void Dispose()
    {
        websocket.Dispose();
    }
}
