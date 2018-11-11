using System;
using System.Text;
using RabbitMQ.Client;

namespace MQSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("logs", ExchangeType.Topic, false, false, null);

            for (int i = 0; i < 10; i++)
            {
                var content = $"catcher wong[{i}]-{Guid.NewGuid().ToString("N")}";
                var sendBytes = Encoding.UTF8.GetBytes(content);
                channel.BasicPublish("logs", "*.logs.*", null, sendBytes);
                Console.WriteLine($"send {content}");
            }

            channel.Close();
            connection.Close();

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
