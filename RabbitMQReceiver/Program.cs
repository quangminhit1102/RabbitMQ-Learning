using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQReceiver;

//var factory = new ConnectionFactory { HostName = "localhost" };
var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//channel.QueueDeclare(queue: "hello",
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);

// // Queue
//QueueConsumer.Consumer(channel);

// // Direct Exchange
//DirectExchangeReceiver.Consumer(channel);

// // Topic Exchange
//TopicExchangeConsumer.Consumer(channel);

// // Header Exchange
//HeaderExchangeConsumer.Consumer(channel);

// // Fanout exchange
FanoutExchangeConsumerMe.Consumer(channel);
FanoutExchangeConsumerYou.Consumer(channel);
