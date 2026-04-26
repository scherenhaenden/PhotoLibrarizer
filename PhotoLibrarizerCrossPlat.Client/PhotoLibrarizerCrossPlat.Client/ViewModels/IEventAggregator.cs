using System;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels;

public interface IEventAggregator
{
    void Publish<TEvent>(TEvent eventToPublish);
    void Subscribe<TEvent>(Action<TEvent> eventHandler);
}