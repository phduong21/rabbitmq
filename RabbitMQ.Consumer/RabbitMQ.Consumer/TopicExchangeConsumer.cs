﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            var ttl = new Dictionary<string, object>()
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic, arguments: ttl);
            channel.QueueDeclare("demo-topic-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind("demo-topic-queue", "demo-topic-exchange", "account.*");
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-topic-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
