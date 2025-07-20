//-----------------------------------------------------------------------
// <copyright file="KafkaManagerAdapter" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>19/07/2025 11:11:05</date>
// <summary>Código fuente interfaz KafkaManagerAdapter.</summary>
//-----------------------------------------------------------------------
namespace ArkanoAdapter
{
    using Confluent.Kafka;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// KafkaManagerAdapter.
    /// </summary>
    public class KafkaManagerAdapter : IQueueManagerAdapter
    {
        #region Attributes

        /// <summary>
        /// Message Receive Event.
        /// </summary>
        event EventHandler<InputMessageEventHandler>? MessageReceiveEvent;

        /// <summary>
        /// object Lock.
        /// </summary>
        object? _objectLock;

        /// <summary>
        /// Attribute used to application Logger.
        /// </summary>
        private readonly ILogger<KafkaManagerAdapter>? _logger;

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private readonly IConfiguration? _configuration;

        /// <summary>
        /// Producer manager.
        /// </summary>
        private IProducer<Null, string>? _producer;

        /// <summary>
        /// Consumer manager.
        /// </summary>
        private IConsumer<Ignore, string>? _consumer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KafkaManagerAdapter"/> class.
        /// </summary>
        /// <param name="configuration">Configuration manager.</param>
        /// <param name="logger">Logger manager.</param>
        public KafkaManagerAdapter(IConfiguration configuration, ILogger<KafkaManagerAdapter> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Object Lock.
        /// </summary>
        object ObjectLock
        {
            get
            {
                if (_objectLock == null)
                    this._objectLock = new object();

                return _objectLock;
            }
        }

        /// <summary>
        /// Receive data event.
        /// </summary>
        event EventHandler<InputMessageEventHandler> IQueueManagerAdapter.OnMessageReceive
        {
            add
            {
                lock (this.ObjectLock)
                {
                    MessageReceiveEvent += value;
                }
            }
            remove
            {
                lock (this.ObjectLock)
                {
                    MessageReceiveEvent -= value;
                }
            }
        }

        /// <summary>
        /// Get Consumer.
        /// </summary>
        public IConsumer<Ignore, string>? Consumer
        {
            get
            {
                if (this._consumer == null)
                {
                    var consumerConfig = new ConsumerConfig
                    {
                        BootstrapServers = this._configuration?.GetSection("BootstrapServers").Value,
                        GroupId = "ArkanoConsumerGroup",
                        AutoOffsetReset = AutoOffsetReset.Earliest
                    };

                    this._consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
                }
                return this._consumer;
            }
        }

        /// <summary>
        /// Get Producer.
        /// </summary>
        public IProducer<Null, string>? Producer
        {
            get
            {
                if (this._producer == null)
                {
                    var producerconfig = new ProducerConfig
                    {
                        BootstrapServers = this._configuration?.GetSection("BootstrapServers").Value
                    };

                    this._producer = new ProducerBuilder<Null, string>(producerconfig).Build();
                }
                return this._producer;
            }
        }

        #endregion

        #region Methods And Functions

        /// <summary>
        /// Method used to dispose all memory.
        /// </summary>
        public void Dispose()
        {
            this.Consumer?.Dispose();
            this.Producer?.Dispose();
        }

        /// <summary>
        /// Method used to Receive Message From Queue.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="stoppingToken">Cancellation token.</param>
        public async void Subscribe(string topic, CancellationToken stoppingToken)
        {
            try
            {
                this.Consumer?.Subscribe(topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await ProcessKafkaMessage();
                    }
                    catch (ConsumeException ex)
                    {
                        this._logger?.LogError($"Error processing Kafka message: {ex.Message}");
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(200), stoppingToken);
                }
            }
            catch (OperationCanceledException ex)
            {
                this._logger?.LogError($"Error processing Kafka message: {ex.Message}");
            }
            finally
            {
                this.Consumer?.Close();
            }
        }

        /// <summary>
        /// Method used to Send Message To Queue.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="message">Message to be send.</param>
        /// <returns></returns>
        public async Task ProduceAsync(string topic, string message)
        {
            var kafkamessage = new Message<Null, string> { Value = message, };
            if (kafkamessage != null && !string.IsNullOrEmpty(topic))
                await this.Producer!.ProduceAsync(topic, kafkamessage);
        }

        /// <summary>
        /// Method used to process message from kafka.
        /// </summary>
        private async Task ProcessKafkaMessage()
        {
            try
            {
                await Task.Run(() =>
                {
                    var consumeResult = this.Consumer?.Consume(TimeSpan.FromMilliseconds(500));
                    if (consumeResult != null)
                    {
                        var message = consumeResult?.Message.Value;
                        this._logger?.LogInformation($"Received inventory update: {message}");

                        if (!string.IsNullOrEmpty(message))
                            this.MessageReceiveEvent?.Invoke(this, new InputMessageEventHandler() { Message = message });
                    }
                })
                .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this._logger?.LogError($"Error processing Kafka message: {ex.Message}");
            }
        }

        #endregion
    }
}
