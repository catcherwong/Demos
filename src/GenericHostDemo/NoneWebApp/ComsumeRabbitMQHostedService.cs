using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NoneWebApp
{
    public class ComsumeRabbitMQHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly AppSettings _settings;
        private IConnection _connection;
        private IModel _channel;

        public ComsumeRabbitMQHostedService(ILoggerFactory loggerFactory, IOptionsSnapshot<AppSettings> options)
        {
            this._logger = loggerFactory.CreateLogger<ComsumeRabbitMQHostedService>();
            this._settings = options.Value;
            InitRabbitMQ(this._settings);
        }

        private void InitRabbitMQ(AppSettings settings)
        {
            var factory = new ConnectionFactory { HostName = settings.HostName, };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_settings.ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_settings.QueueName, false, false, false, null);
            _channel.QueueBind(_settings.QueueName, _settings.ExchangeName, _settings.RoutingKey, null);
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);
                HandleMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_settings.QueueName, false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            _logger.LogInformation($"consumer received {content}");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation($"connection shut down {e.ReplyText}");
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation($"consumer cancelled {e.ConsumerTag}");
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation($"consumer unregistered {e.ConsumerTag}");
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            _logger.LogInformation($"consumer registered {e.ConsumerTag}");
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation($"consumer shutdown {e.ReplyText}");
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
