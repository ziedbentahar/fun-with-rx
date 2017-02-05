using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Reactive.Linq;
using System.Text;

namespace Common
{
    public class Subscriber
    {
        private readonly IModel _channel;
        private readonly ISerializer<string> _serializer;
        public Subscriber(IModel channel, ISerializer<string> serializer)
        {
            _channel = channel;
            _serializer = serializer;
        }

        public IObservable<T> GetEventStream<T>(string endpoint)
        {
            return Observable.Create<T>(observer =>
            {
                var typeName = typeof(T).FullName;

                _channel.ExchangeDeclare(exchange: endpoint, type: "fanout");
                var queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: queueName, exchange: endpoint, routingKey: string.Empty);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (s, e) =>
                {
                    var body = e.Body;
                    object bodyType;
                    if (e.BasicProperties.Headers.TryGetValue("Body-Type", out bodyType)
                        && typeName.Equals(Encoding.UTF8.GetString((byte[])bodyType), StringComparison.OrdinalIgnoreCase))

                    {
                        observer.OnNext(_serializer.Deserialize<T>(Encoding.UTF8.GetString(e.Body)));
                    }

                };

                var consumerTag = _channel.BasicConsume(queueName, false, consumer);

                return () => { _channel.BasicCancel(consumerTag); };
            })
            .Publish().RefCount();
        }
    }

    
}
