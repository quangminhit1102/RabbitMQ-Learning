﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQSender
{
    public static class TopicExchangePublisher
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare("topic-exchange", type: ExchangeType.Topic);

            int count = 0;
            while (true)
            {
                var message = $"Message Number {count}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "topic-exchange",
                                     routingKey: "routingKey.abc",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" [x] Sent {message}");
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
