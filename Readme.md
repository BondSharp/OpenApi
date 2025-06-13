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
var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("hostsettings.json");
builder
    .AddDefaultTimeProvider()
    .AddAlor();

using IHost host = builder.Build();
var cancellationTokenSource = new CancellationTokenSource();
var task = host.RunAsync(cancellationTokenSource.Token);
```

## Получение инструментов

```C#
var instrumentProvider = host.Services.GetRequiredService<IInstrumentsProvider>();
var sber =  await instrumentProvider.Get<IShare>("sber");
var futures = instrumentProvider.GetFutures();
var shares = instrumentProvider.GetShares();
var americanOptions = instrumentProvider.GetAmericanOptions();
var europeanOptions = instrumentProvider.GetEuropeanOptions();
```

## Получение сделок
```C#
var dealsProvider = host.Services.GetRequiredService<IDealsProvider>();
var dealsPast = await dealsProvider.GetPast(sber).ToArrayAsync();
var dealsToday = await dealsProvider.GetToday(sber).ToArrayAsync();
```

## Маркер дата в реальном времени 

``` C#
var marketData = host.Services.GetRequiredService<IDataMarket>();
marketData.Events.Subscribe(@event => Console.WriteLine(@event), error => Console.WriteLine(error));
marketData.SubscribeOrderBook(sber);
marketData.SubscribeDeal(sber);
marketData.SubscribeInstrumentChanged(sber);
```