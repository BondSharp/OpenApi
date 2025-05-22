﻿using System.Text.Json.Serialization;
using BondSharp.OpenApi.Core.Data;

namespace BondSharp.OpenApi.Alor.Data;
internal class InstrumentChanged : IInstrumentChanged
{
    [JsonPropertyName("st")]
    [JsonConverter(typeof(JsonNumberEnumConverter<TradingStatus>))]
    public TradingStatus? Status { get; init; }

    [JsonPropertyName("pxmn")]
    public double? PriceMin { get; init; }

    [JsonPropertyName("pxmx")]
    public double? PriceMax { get; init; }

    [JsonIgnore]
    public DateTime ReceivedAt { get; set; } = DateTime.Now;
}
