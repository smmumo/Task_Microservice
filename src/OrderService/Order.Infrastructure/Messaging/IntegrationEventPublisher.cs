using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.Messaging.Settings;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Application.Messaging;
using RabbitMQ.Client;

namespace Order.Infrastructure.Messaging
{
    /// <summary>
    /// Represents the integration event publisher.
    /// </summary>
    internal sealed class IntegrationEventPublisher :  IIntegrationEventPublisher, IDisposable
    {
        private readonly MessageBrokerSettings _messageBrokerSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<IntegrationEventPublisher> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEventPublisher"/> class.
        /// </summary>
        /// <param name="messageBrokerSettingsOptions">The message broker settings options.</param>
        public IntegrationEventPublisher(IOptions<MessageBrokerSettings> messageBrokerSettingsOptions,
                ILogger<IntegrationEventPublisher> logger)
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
        }

        public void Publish(object integrationEvent)
        {
            string payload = JsonSerializer.Serialize(integrationEvent);

            byte[] body = Encoding.UTF8.GetBytes(payload);

            _channel.BasicPublish(string.Empty, _messageBrokerSettings.QueueName, body: body);

            _logger.LogInformation("Published integration event to queue {QueueName}: {Event}", _messageBrokerSettings.QueueName, payload);
           
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection?.Dispose();

            _channel?.Dispose();
        }
    }
}