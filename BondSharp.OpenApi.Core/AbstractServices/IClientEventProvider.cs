﻿using BonadSharp.OpenApi.Core.Instruments;
using BondSharp.OpenApi.Core.Events;

namespace BondSharp.OpenApi.Core.AbstractServices;
public interface IClientEventProvider
{
    IObservable<IEvent> Events { get; }
    IObservable<ReconnectionEvent> Reconnections { get; }
    void SubscribeOrderBook(IInstrument instrument, int depth);
    void SubscribeDeal(IInstrument instrument);
    void SubscribeInstrumentChanged(IInstrument instrument);
    void Ping();
    void Resubscribe();

}
