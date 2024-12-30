
using AlorClient;
using BonadSharp.OpenApi.Core.AbstractServices;
using BondSharp.OpenApi.Abstract;
using BondSharp.OpenApi.Alor.Authorization;
using BondSharp.OpenApi.Alor.Common;
using BondSharp.OpenApi.Alor.Deals;
using BondSharp.OpenApi.Alor.Instruments;
using BondSharp.OpenApi.Alor.Subscriptions;
using BondSharp.OpenApi.Core.AbstractServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Websocket.Client;

namespace BondSharp.OpenApi.Alor;

public static class ServiceRegistration
{
    private const string configKey = "AlorClient";

    public static IHostApplicationBuilder AddAlor(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var config = builder.Configuration.GetSection(configKey);
        services
           .Configure<Settings>(config)
           .AddSingleton<Settings>(provider => provider.GetRequiredService<IOptions<Settings>>().Value)
           .AddCommon()
           .AddSubscriptions()
           .AddProviers();

        return builder;
    }

    private static IServiceCollection AddProviers(this IServiceCollection services)
    {
        return
            services.AddTransient<IInstrumentsProvider, InstrumentsProvider>()
            .AddTransient<IDealsProvider, DealsProvider>();
    }

    private static IServiceCollection AddSubscriptions(this IServiceCollection services)
    {
        return services
            .AddTransient<IIDataMarketBuilder, SubscriptionsBuilder>()
            .AddTransient<WebSocketClientFactory>()
            .AddScoped<IWebsocketClient>(x => x.GetRequiredService<WebSocketClientFactory>().Factory())
            .AddScoped<RequestsSubscriber>()
            .AddScoped<EventsProvider>();
    }

    private static IServiceCollection AddCommon(this IServiceCollection services)
    {
        return services
               .AddSingleton<TokenAuthorization>()
              .AddSingleton<ApiClient>()
              .AddScoped<IInstrumentsProvider, InstrumentsProvider>()
              .AddHostedService<UpdatingToken>();
    }
}
