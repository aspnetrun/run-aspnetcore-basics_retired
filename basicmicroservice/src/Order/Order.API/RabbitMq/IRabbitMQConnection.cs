using RabbitMQ.Client;
using System;

namespace Order.API.RabbitMq
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
