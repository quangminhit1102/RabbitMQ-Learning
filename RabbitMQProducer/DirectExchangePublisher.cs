using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    public static class DirectExchangePublisher
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("direct-exchange", type: ExchangeType.Direct);

            int count = 0;
            while (true)
            {
                var message = $"Message Number {count}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "direct-exchange",
                                     routingKey: "CRoutingKey",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" [x] Sent {message}");
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
