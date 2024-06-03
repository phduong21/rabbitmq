using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://blgozgyn:Q56JrHZuwX-4qqm4qeKX5uItj0fjqYnV@goose.rmq2.cloudamqp.com/blgozgyn")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            //QueueProducer.Publish(channel);
            //DirectExchangePublisher.Publish(channel);
            TopicExchangePublisher.Publish(channel);
        }
    }
}
