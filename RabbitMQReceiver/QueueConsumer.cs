using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
    public class QueueConsumer
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Consumer(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}
