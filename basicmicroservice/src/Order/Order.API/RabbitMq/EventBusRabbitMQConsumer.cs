using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Order.API.RabbitMq
{
    public class EventBusRabbitMQConsumer
    {        
        const string QUEUE_NAME = "basketCheckoutQueue";

        private readonly IRabbitMQConnection _connection;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: QUEUE_NAME, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
          
            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: QUEUE_NAME, autoAck: true, consumer: consumer);            
        }

        private void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == QUEUE_NAME)
            {
                var message = Encoding.UTF8.GetString(e.Body);
                BasketCheckout basketCheckout = JsonConvert.DeserializeObject<BasketCheckout>(message);
                
                // TODO : stuff
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }

    public class BasketCheckout
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardTypeId { get; set; }
        public string Buyer { get; set; }
        public Guid RequestId { get; set; }
    }
}
