# Библиотека для работы с [Alor Open Api](https://alor.dev/docs)  на языке C#
## Пример appsettings.json

```json
{
  "AlorClient" : {  
	"RefreshToken" : "4876c5f4-b51b-4ae2-99d2-d8df6bfe22d5",
	"IsProduction" : true,
	"ReconnectTimeout" : "0:01:00",
	"ErrorReconnectTimeout" : "0:01:00",
	"RefreshingTokenTimeout" : "0:10:00"
  }	
}
```

## AlorClient

- RefreshToken получить можно по [ссылки](https://alor.dev/open-api-tokens)
- IsProduction являеться ли RefreshToken от production
- ReconnectTimeout время ожидания сообщение от WebSocket
- ErrorReconnectTimeout время ожидания для повторно соедение с WebSocket
- RefreshingTokenTimeout  время ожидания обновления token

## Пример подключение библиотеки 
```C#
using BonadSharp.OpenApi.Core.AbstractServices;
using BondSharp.OpenApi.Abstract;
using BondSharp.OpenApi.Alor;
using BondSharp.OpenApi.Domain.Instruments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("hostsettings.json");
builder.AddAlor(builder.Configuration);
using IHost host = builder.Build();
await host.RunAsync();
```

## Получение инструмента 

```C#
var instrumentProvider = host.Services.GetRequiredService<IInstrumentsProvider>();
var instrument = await instrumentProvider.All(TimeSpan.FromDays(1)).OfType<IShare>().FirstAsync(x => x.Symbol == "SBER");
```

## Маркер дата в реальном времени 

``` C#
using var dataMarket = host.Services
    .GetRequiredService<IIDataMarketBuilder>()
    .SubscribeDeal(instrument)
    .SubscribeOrderBook(instrument)
    .Build();

dataMarket.Events.Subscribe(@event => Console.WriteLine(@event));
```