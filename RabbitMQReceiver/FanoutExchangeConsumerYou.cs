using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQReceiver
{
    public static class FanoutExchangeConsumerYou
    {
        /// <summary>
        /// publish message to queue.
        /// </summary>
        /// <param name="channel"></param>
        public static void Consumer(IModel channel)
        {
            channel.ExchangeDeclare("fanout-exchange", type: ExchangeType.Fanout);
            channel.QueueDeclare("fanout-exchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
            channel.QueueBind("fanout-exchange-queue", "fanout-exchange", "fanoutRountingKey");
            //channel.BasicQos(0, 10, false);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
            };
            channel.BasicConsume(queue: "fanout-exchange-queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}
