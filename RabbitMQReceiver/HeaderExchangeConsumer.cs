using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    public static class HeaderExchangeConsumer
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("header-exchange", type: ExchangeType.Headers);
            channel.QueueDeclare("header-exchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            // Header dictionary
            Dictionary<string,object> dictionary = new Dictionary<string,object>();
            dictionary.Add("header", "exchange");

            channel.QueueBind("header-exchange-queue", "header-exchange", "headerRoutingKey", dictionary);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
            };
            channel.BasicConsume(queue: "header-exchange-queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}
