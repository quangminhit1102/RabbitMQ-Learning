using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    public static class TopicExchangeConsumer
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("topic-exchange", type: ExchangeType.Topic);
            channel.QueueDeclare("topic-exchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
            channel.QueueBind("topic-exchange-queue", "topic-exchange", "routingKey.pattern");


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
            };
            channel.BasicConsume(queue: "topic-exchange-queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}
