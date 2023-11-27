using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueProducer
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Publish(IModel channel)
        {
            int count = 0;
            while (true)
            {
                var message = $"Message Number {count}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" [x] Sent {message}");
                Thread.Sleep(1000);
                count++;
            }
        }
    }
}
