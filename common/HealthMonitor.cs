using RabbitMQ.Client;
using System;
using System.Reactive.Linq;

namespace Common
{





    /*public class HealthMonitor
    {
        private readonly IModel _channel;
        public HealthMonitor(IModel channel)
        {
            _channel = channel;
        }

        public void Start()
        {
            _channel.ExchangeDeclare(exchange: "SystemHealth", type: "fanout");
            var queue = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _channel.QueueDeclare().QueueName,
                exchange: "SystemHealth", routingKey: string.Empty);
        }

        private IObservable<string> HeartbeatMessageStream
        {
            return Observable.Cre
        }

        public IObservable<ServiceStatus> ObserveServiceStatus(string serviceId)
        {
            return Observable.Create<ServiceStatus>(observer =>
            {


                return () =>
                {

                }; // Dispose action
            })
            
            .Timeout(TimeSpan.FromSeconds(10))
            .Catch((TimeoutException tex) => { return Observable.Return(ServiceStatus.Down); })
            .Repeat()
            .StartWith(ServiceStatus.Unknown)
            .DistinctUntilChanged();
        }
    }

    public class ServiceBase<TRequest, TReply>
    {
        public ServiceBase()
        {

        }

        public IObservable<TReply> SendRequest(TRequest request)
        {

        }
    }

    public enum ServiceStatus
    {
        Unknown,
        Up,
        Unhealthy,
        Down
    }*/
}
