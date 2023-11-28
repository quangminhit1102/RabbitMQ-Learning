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

//QueueConsumer.Consumer(channel);

DirectExchangeReceiver.Consumer(channel);
