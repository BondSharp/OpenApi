using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Core.AbstractServices;
using System.Reactive.Linq;
using BonadSharp.OpenApi.Core.Instruments;
using BonadSharp.OpenApi.Core.Data;
using BondSharp.OpenApi.Core.Data;
using BondSharp.OpenApi.Alor.Data;
using BondSharp.OpenApi.Alor.Orders.Data;
using System.Reactive.Threading.Tasks;

namespace BondSharp.OpenApi.Alor.Orders;
internal class OrderManager : IOrderManager
{
    private readonly TokenAuthorization tokenAuthorization;
    private readonly OrderClient client;
    private Portfolio portfolio;

    public OrderManager(TokenAuthorization tokenAuthorization, OrderClient client)
    {
        this.tokenAuthorization = tokenAuthorization;
        this.client = client;
        var account = tokenAuthorization.GetAccounts().Single(x => x.Number.StartsWith("D"));
        portfolio = account;
    }
    public async Task LogIn()
    {
        var accessToken = tokenAuthorization.Token().AccessToken;
        var request = new AuthorizeRequest()
        {
            OperationCode = "authorize",
            Token = accessToken,
        };
        await Send(request);
    }

    public async Task<string> Market(IInstrument instrument, int volume, Side side, string? oldOrderId)
    {

        var orderPost = new OrderPostRequest()
        {
            InstrumentId = InstrumentId.Factory(instrument),
            Volume = volume,
            Side = side,
            OperationCode = oldOrderId == null ? "create:market" : "update:market",
            OrderId = oldOrderId,
            Portfolio = portfolio
        };
        var result = await SendOrder(orderPost);

        return result;
    }

    public async Task<string> Limit(IInstrument instrument, int volume, Side side, double price, string? oldOrderId)
    {
        var orderPost = new OrderPostRequest()
        {
            InstrumentId = InstrumentId.Factory(instrument),
            Volume = volume,
            Side = side,
            OperationCode = oldOrderId == null ? "create:limit" : "update:limit",
            OrderId = oldOrderId,
            Portfolio = portfolio,
            Price = price
        };
        var result = await SendOrder(orderPost);

        return result;
    }

    public async Task<string> Stop(IInstrument instrument, int volume, Side side, ConditionPrice conditionPrice, double triggerPrice, string? oldOrderId)
    {
        var orderPost = new OrderPostRequest()
        {
            InstrumentId = InstrumentId.Factory(instrument),
            Volume = volume,
            Side = side,
            OperationCode = oldOrderId == null ? "create:stop" : "update:stop",
            OrderId = oldOrderId,
            Portfolio = portfolio,
            Condition = conditionPrice,
            TriggerPrice = triggerPrice
        };
        var result = await SendOrder(orderPost);

        return result;
    }

    public async Task<string> StopLimit(IInstrument instrument, int volume, Side side, ConditionPrice conditionPrice, double price, double triggerPrice, string? oldOrderId)
    {
        var orderPost = new OrderPostRequest()
        {
            InstrumentId = InstrumentId.Factory(instrument),
            Volume = volume,
            Side = side,
            OperationCode = oldOrderId == null ? "create:stopLimit" : "update:stopLimit",
            OrderId = oldOrderId,
            Portfolio = portfolio,
            Condition = conditionPrice,
            TriggerPrice = triggerPrice,
            Price = price
        };
        var result = await SendOrder(orderPost);

        return result;
    }

    public Task DeleteMarket(string orderId)
    {
        return Delete("delete:market", orderId);
    }

    public Task DeleteLimit(string orderId)
    {
        return Delete("delete:limit", orderId);
    }

    public Task DeleteStop(string orderId)
    {
        return Delete("delete:stop", orderId);
    }

    public Task DeleteStopLimit(string orderId)
    {
        return Delete("delete:stopLimit", orderId);
    }

    private Task Delete(string operationName, string orderId)
    {
        var deleteRequest = new DeleteRequest() { OperationCode = operationName, OrderId = orderId, Portfolio = portfolio };
        return SendOrder(deleteRequest);
    }

    private async Task<string> SendOrder(BaseRequest request)
    {
        var responce = (OrderNumberResponse)(await Send(request));

        return responce.OrderId;
    }

    private async Task<EmptyResponce> Send(BaseRequest request)
    {
        var responce = GetResponce(request);
        client.Send(request);
        return await responce;
    }

    private async Task<EmptyResponce> GetResponce(BaseRequest request)
    {
        var result = await client.Responces
            .FirstAsync(responce => responce.RequestGuid == request.Guid)
            .Timeout(TimeSpan.FromSeconds(3)).ToTask();

        if (result.Code != 200)
        {
            throw new Exception($"Responce {result.Message} with code {result.Code}");
        }
        ;
        return result;
    }
}
