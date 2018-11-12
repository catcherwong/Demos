using System;
using System.Text;
using RabbitMQ.Client;

namespace MQSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");

                for (int i = 0; i < 20; i++)
                {
                    var message = string.Concat("catcherwong-", $"{i.ToString()}---{Guid.NewGuid()}");
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "topic_logs",
                                   routingKey: "*.log.#",
                                   basicProperties: null,
                                   body: body);
                    Console.WriteLine(" [x] Sent '{0}':'{1}'", "*.log.#", message);
                }
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
