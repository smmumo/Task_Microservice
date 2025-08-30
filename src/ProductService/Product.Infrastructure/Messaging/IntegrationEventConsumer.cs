using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.Messaging.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Product.Domain.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Product.Infrastructure.Messaging
{
    internal sealed class IntegrationEventConsumer
    {
        private readonly MessageBrokerSettings _messageBrokerSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<IntegrationEventConsumer> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEventConsumer"/> class.
        /// </summary>
        /// <param name="messageBrokerSettingsOptions">The message broker settings options.</param>
        public IntegrationEventConsumer(IOptions<MessageBrokerSettings> messageBrokerSettingsOptions, ILogger<IntegrationEventConsumer> logger)
        {
            _messageBrokerSettings = messageBrokerSettingsOptions.Value;
            _logger = logger;

            IConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = _messageBrokerSettings.HostName,
                Port = _messageBrokerSettings.Port,
                UserName = _messageBrokerSettings.UserName,
                Password = _messageBrokerSettings.Password
            };

            _connection = connectionFactory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_messageBrokerSettings.QueueName, false, false, false);

        //     _channel.QueueDeclare(queue: "inventory_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        // _   _channel.QueueBind(queue: "inventory_queue", exchange: "order_exchange", routingKey: "");
        }

        public void Consume()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Received message from queue {QueueName}: {Message}", _messageBrokerSettings.QueueName, message);

                var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedIntegrationEvent>(message);

                _logger.LogInformation("Processed OrderCreatedIntegrationEvent: OrderId={OrderId}, ProductName={ProductName}, Quantity={Quantity}",
                    orderCreatedEvent.OrderId, orderCreatedEvent.ProductName, orderCreatedEvent.Quantity);
            };
            _channel.BasicConsume(_messageBrokerSettings.QueueName, true, consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}