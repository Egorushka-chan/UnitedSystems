using System.Text.Json;

using Confluent.Kafka;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.EventBus.Interfaces;
using UnitedSystems.EventBus.Models;

namespace UnitedSystems.EventBus.Kafka
{
    internal class KafkaEventBus : BackgroundService, IEventBus, IDisposable
    {
        public KafkaEventBus(
            IOptions<KafkaEventBusSettings> busSettings, 
            IServiceProvider services,
            IOptions<EventBusSubscriptionInfo> subscriptionInfo,
            ILogger<KafkaEventBus> logger)
        {
            this.busSettings = busSettings.Value;
            this.services = services;
            this.logger = logger;
            this.subscriptions = subscriptionInfo.Value;

            producerConfig = new ProducerConfig {
                BootstrapServers = this.busSettings.HostName,
                SaslUsername = this.busSettings.UserName,
                SaslPassword = this.busSettings.Password,
                Acks = Acks.All
            };

            consumerConfig = new ConsumerConfig(producerConfig)
            {
                GroupId = this.busSettings.ServiceQueueName
            };
        }

        private readonly KafkaEventBusSettings busSettings;
        private readonly IServiceProvider services;
        private readonly ILogger<KafkaEventBus> logger;
        private readonly EventBusSubscriptionInfo subscriptions;

        private readonly ProducerConfig producerConfig;
        private readonly ConsumerConfig consumerConfig;

        private IConsumer<Null, string>? consumer;
        private IProducer<Null, string>? producer;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Factory.StartNew(async () => {
                consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();

                IEnumerable<string> eventNames = subscriptions.EventTypes.Keys;

                consumer.Subscribe(eventNames);

                while (!stoppingToken.IsCancellationRequested) {
                    try {
                        var consumeResult = consumer.Consume(stoppingToken);
                        logger.LogDebug("Успешно получено сообщение Kafka. Начинается обратотка");
                        await OnMessageReceive(consumeResult);
                    }
                    catch (OperationCanceledException) {
                        break;
                    }
                    catch (ConsumeException e) {
                        if (e.Error.IsFatal) {
                            logger.LogCritical("Критическая ошибка поглощения сообщения! Message: {message}, Data: {data}",
                                e.Message, e.Data);
                            break;
                        }
                        else {
                            logger.LogWarning("Ошибка поглощения сообщения. Message: {message}, Data: {data}", e.Message, e.Data);
                        }
                    }
                    catch (Exception e) {
                        logger.LogError("Неизвестная ошибка при поглощении сообщения!. Message: {message}, Data: {data}", e.Message, e.Data);
                        break;
                    }
                }

            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

            logger.LogInformation("Kafka consumer is now online");
            return Task.CompletedTask;
        }

        private async Task OnMessageReceive(ConsumeResult<Null, string> consumeResult)
        {
            string routingKey = consumeResult.Topic;
            string message = consumeResult.Message.Value;

            logger.LogInformation("Получено сообщение из топика {topic}. Message:\n" +
                "{message}", routingKey, message);

            if (!subscriptions.EventTypes.TryGetValue(routingKey, out var valueType)) {
                throw new InvalidOperationException("Нет подобных зарегистрированных обработчиков");
            }
            var eventHandlers = services.GetKeyedServices<IIntegrationEventHandler>(valueType);

            IntegrationEvent value = JsonSerializer.Deserialize(message, valueType, subscriptions.JsonSerializerOptions) as IntegrationEvent
                ?? throw new JsonException("Не удалось десериализовать сообщение");

            foreach (var eventHandler in eventHandlers) {
                await eventHandler.Handle(value);
            }
        }

        public async Task PublishAsync(IntegrationEvent @event)
        {
            string topic = @event.GetType().GetKey();

            producer ??= new ProducerBuilder<Null, string>(producerConfig).Build();

            string body = JsonSerializer.Serialize(@event, @event.GetType(), subscriptions.JsonSerializerOptions);
            var message = new Message<Null, string>() {
                Value = body
            };

            logger.LogInformation("Оправка сообщения в топик {topic}:\n" +
                "{body}", topic, body);

            var result = await producer.ProduceAsync(topic, message);

            switch (result.Status) {
                case PersistenceStatus.NotPersisted:
                    logger.LogWarning("Сообщение {type} не было доставлено", topic);
                    throw new InvalidOperationException($"Сообщение {topic} не было доставлено");
                case PersistenceStatus.PossiblyPersisted:
                    logger.LogInformation("Сообщение {type} в неизвестном состоянии", topic);
                    break;
                case PersistenceStatus.Persisted:
                    logger.LogDebug("Сообщение {type} доставлено", topic);
                    break;
            }
        }

        public override void Dispose()
        {
            if(consumer != null) {
                consumer.Dispose();
                consumer = null;
            }
            
            if(producer != null) {
                producer.Dispose();
                producer = null;
            }
            base.Dispose();
        }
    }
}
