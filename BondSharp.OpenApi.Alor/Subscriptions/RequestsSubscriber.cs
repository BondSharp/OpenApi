﻿using System.Text.Json;
using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Alor.Subscriptions.Requests;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor.Subscriptions;
internal class RequestsSubscriber(TokenAuthorization tokenAuthorization, IWebsocketClient client)
{
    private readonly Dictionary<Guid, BaseRequest> requests = new Dictionary<Guid, BaseRequest>();

    public BaseRequest GetRequest(Guid guid)
    {
        return requests[guid];
    }

    public void AddRequest(BaseRequest request)
    {
        if (requests.ContainsKey(request.Guid))
        {
            throw new ArgumentException($"Duplicate {nameof(request.Guid)} of {nameof(request)}");
        }
        requests.Add(request.Guid, request);

    }

    public void Subscribe()
    {
        foreach (var request in requests.Values)
        {
            Push(request);
        }
    }

    private void Push(BaseRequest request)
    {
        var token = tokenAuthorization.Token();
        request.Token = token.AccessToken;
        var json = JsonSerializer.Serialize(request, request.GetType());
        client.Send(json);
    }
}