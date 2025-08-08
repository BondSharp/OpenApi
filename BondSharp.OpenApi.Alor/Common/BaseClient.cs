using System.Net.WebSockets;
using System.Reactive.Linq;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Common;
internal abstract class BaseClient : IDisposable
{
    private IWebsocketClient websocket;

    protected abstract string ProductionAddress { get; }
    protected abstract string DevelopmentAddress { get; }

    public BaseClient(Settings settings)
    {
        websocket = GetWebsocket(settings);
    }
    protected IObservable<string> Messages => websocket.MessageReceived.Select(x => x.Text!);
    public IObservable<ReconnectionType> ReconnectionTypes => websocket.ReconnectionHappened.Select(x => x.Type);

    protected void Send(string json) => websocket.Send(json);

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
