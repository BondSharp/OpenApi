using BondSharp.OpenApi.Core.AbstractServices;
using BondSharp.OpenApi.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BondSharp.OpenApi.Core;
public static class ServiceRegistration
{
    public static IHostApplicationBuilder AddDefaultTimeProvider(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ITimeProvider, DefaultTimeProvider>();

        return builder;
    }
}
