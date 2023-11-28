using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    public static class DirectExchangeReceiver
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("direct-exchange", type: ExchangeType.Direct);
            channel.QueueDeclare("direct-exchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
            channel.QueueBind("direct-exchange-queue", "direct-exchange", "CRoutingKey");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
            };
            channel.BasicConsume(queue: "direct-exchange-queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}
