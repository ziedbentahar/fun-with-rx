using Common;
using RabbitMQ.Client;
using System;
using System.Reactive.Linq;

namespace ConsoleApplication
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
                Subscriber subscriber = new Subscriber(channel, serializer);

                subscriber.GetEventStream<Common.Dtos.ServiceStatus>("my-exchange")
                    .Where(serviceStatus => serviceStatus.ServiceId == "service-2")
                    .DistinctUntilChanged(s => s.Status)
                    .Subscribe(status =>
                    {
                        Console.WriteLine(status);
                    },
                    error =>
                    {
                        Console.WriteLine(error);
                    }, 
                    () => { Console.WriteLine("Completed"); });

                Console.ReadLine();

            }
            
        }
    }
}
