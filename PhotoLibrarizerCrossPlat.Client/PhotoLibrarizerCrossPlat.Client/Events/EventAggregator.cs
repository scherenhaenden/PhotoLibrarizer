using System;
using System.Collections.Generic;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels;

public class EventAggregator : IEventAggregator
{
    private readonly Dictionary<Type, List<object>> _subscribers = new();

    public void Publish<TEvent>(TEvent eventToPublish)
    {
        if (_subscribers.TryGetValue(typeof(TEvent), out var handlers))
        {
            foreach (var handler in handlers)
            {
                ((Action<TEvent>)handler)(eventToPublish);
            }
        }
    }

    public void Subscribe<TEvent>(Action<TEvent> eventHandler)
    {
        if (!_subscribers.ContainsKey(typeof(TEvent)))
        {
            _subscribers[typeof(TEvent)] = new List<object>();
        }

        _subscribers[typeof(TEvent)].Add(eventHandler);
    }
}