using RabbitMQ.Client;
using System;

namespace Basket.API.RabbitMq
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();        
    }
}
