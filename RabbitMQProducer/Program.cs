using System.Text;
using RabbitMQ.Client;
using RabbitMQSender;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//channel.QueueDeclare(queue: "hello",
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);

// // Queue
//QueueProducer.Publish(channel);

// // Direct Exchange Publisher
//DirectExchangePublisher.Publish(channel);

// // Topic Exchange
//TopicExchangePublisher.Publish(channel);

// // Header Exchange
HeaderExchangePublisher.Publish(channel);

