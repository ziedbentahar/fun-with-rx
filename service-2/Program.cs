using Common;
using RabbitMQ.Client;
using System;
using System.Linq;
using Common.Dtos;
using System.Reactive.Linq;

namespace service_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JsonSerializer serializer = new JsonSerializer();
            var factory = new ConnectionFactory();
            factory.SetUri(new Uri("amqp://guest:guest@192.168.99.100:5672/"));
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var publisher = new Publisher(channel, serializer);

                Observable.Interval(TimeSpan.FromSeconds(5))
                    .SubscribeOn(System.Reactive.Concurrency.TaskPoolScheduler.Default)
                    .Select((ts, index) =>
                    {
                        if (index % 12 == 0)
                            return new ServiceStatus { ServiceId = "service-2", Status = "Oh my ! I am on a bad shape" };
                        else
                            return new ServiceStatus { ServiceId = "service-2", Status = "Healthy as a horse" };
                    })
                    .Subscribe(status =>
                    {
                        publisher.Publish(status, "my-exchange");
                    });
                   

                

                Console.ReadLine();

            }
        }
    }
}
