using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQSender
{
    public static class HeaderExchangePublisher
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("header-exchange", type: ExchangeType.Headers);

            int count = 0;
            while (true)
            {
                var message = $"Message Number {count}";
                var body = Encoding.UTF8.GetBytes(message);

                // Add heaer properties.
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>();
                properties.Headers.Add("header", "exchange");

                channel.BasicPublish(exchange: "header-exchange",
                                     routingKey: string.Empty,
                                     basicProperties: properties,
                                     body: body);
                Console.WriteLine($" [x] Sent {message}");
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
