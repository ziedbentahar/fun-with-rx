using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Common
{
    public class Publisher
    {
        private readonly IModel _channel;
        private readonly ISerializer<string> _serializer;
        public Publisher(IModel channel, ISerializer<string> serializer)
        {
            _channel = channel;
            _serializer = serializer;
        }

        public void Publish<T>(T dto, string endpoint)
        {
            _channel.ExchangeDeclare(exchange: endpoint, type: "fanout");
            var message = System.Text.Encoding.UTF8.GetBytes(_serializer.Serialize(dto));
            var messageProperties = _channel.CreateBasicProperties();
            messageProperties.Headers = new Dictionary<string, object>
            {
                ["Body-Type"] = typeof(T).FullName

            };

            _channel.BasicPublish(exchange: endpoint, routingKey: string.Empty, basicProperties: messageProperties, body: message);
        }
    }

}
