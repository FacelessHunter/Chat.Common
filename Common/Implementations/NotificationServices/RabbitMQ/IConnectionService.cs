using RabbitMQ.Client;
using System;

namespace Common.Implementations.NotificationServices.RabbitMQ
{
    public interface IConnectionService : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();

    }
}
