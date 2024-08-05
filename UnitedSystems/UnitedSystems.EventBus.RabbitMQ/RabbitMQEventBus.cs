using System;
using System.Text;
using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using UnitedSystems.EventBus.Events;
using UnitedSystems.EventBus.Interfaces;
using UnitedSystems.EventBus.Models;

namespace UnitedSystems.EventBus.RabbitMQ
{
    public class RabbitMQEventBus
        (IOptions<EventBusSettings> busSettings,
        IServiceProvider services,
        IOptions<EventBusSubscriptionInfo> subscriptionsOptions) 
        : BackgroundService, IEventBus, IDisposable
    {
        private IModel? _consumerChannel;
        private IConnection? _rabbitConnection;

        private EventBusSettings busSettings = busSettings.Value;
        private EventBusSubscriptionInfo subscriptions = subscriptionsOptions.Value;

        private string _consumerChannelQueue = busSettings.Value.ServiceQueueName;
        private const string _exchangeName = "event_bus_exchange";

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Factory.StartNew(() => {
                _rabbitConnection = services.GetRequiredService<IConnection>();
                if (!_rabbitConnection.IsOpen) {
                    throw new InvalidOperationException("Не удалось открыть подключение");
                }

                _consumerChannel = _rabbitConnection.CreateModel();
                _consumerChannel.ExchangeDeclare(exchange: _exchangeName,
                                        type: "direct");

                _consumerChannel.QueueDeclare(queue: _consumerChannelQueue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += OnMessageReceived;

                _consumerChannel.BasicConsume(
                    queue: _consumerChannelQueue,
                    autoAck: false,
                    consumer: consumer);

                foreach (var (eventName, _) in subscriptions.EventTypes) {
                    _consumerChannel.QueueBind(
                        queue: _consumerChannelQueue,
                        exchange: _exchangeName,
                        routingKey: eventName);
                }

            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            return Task.CompletedTask;
        }

        public Task PublishAsync(IntegrationEvent @event)
        {
            var routingKey = @event.GetType().Name;

            using var channel = _rabbitConnection?.CreateModel() ?? throw new InvalidOperationException("Нет подключения к RabbitMQ");

            channel.ExchangeDeclare(exchange: _exchangeName, type: "direct");

            var properties = channel.CreateBasicProperties();
            // persistent - сохраняет на диск
            properties.DeliveryMode = 2;

            var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), subscriptions.JsonSerializerOptions);

            channel.BasicPublish(exchange: _exchangeName,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: properties,
                body: body);

            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(object sender, BasicDeliverEventArgs @event)
        {
            string routingKey = @event.RoutingKey;
            string message = Encoding.UTF8.GetString(@event.Body.Span);

            if(!subscriptions.EventTypes.TryGetValue(routingKey, out var valueType)) {
                throw new InvalidOperationException("Нет подобных зарегистрированных обработчиков");
            }

            var eventHandlers = services.GetKeyedServices<IIntegrationEventHandler>(valueType);

            IntegrationEvent value = JsonSerializer.Deserialize(message, valueType, subscriptions.JsonSerializerOptions) as IntegrationEvent
                ?? throw new JsonException("Не удалось десериализовать сообщение");

            foreach (var eventHandler in eventHandlers) {
                await eventHandler.Handle(value);
            }

            _consumerChannel.BasicAck(@event.DeliveryTag, multiple: false);
        }
    }
}
