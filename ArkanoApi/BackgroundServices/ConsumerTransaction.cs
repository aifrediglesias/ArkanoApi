//-----------------------------------------------------------------------
// <copyright file="ConsumerTransaction" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifre</author>
// <date>19/07/2025 11:07:32</date>
// <summary>Código fuente interfaz ConsumerTransaction.</summary>
//-----------------------------------------------------------------------
namespace ArkanoApi.BackgroundServices
{
    using ArkanoAdapter;
    using ArkanoBussiness;
    using ArkanoBussiness.Dtos;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// ConsumerTransaction.
    /// </summary>
    internal class ConsumerTransaction : BackgroundService
    {
        #region Attributes

        /// <summary>
        /// QueueManagerAdapter.
        /// </summary>
        private readonly IQueueManagerAdapter? _queueManagerAdapter;

        /// <summary>
        /// Attribute used to application Logger.
        /// </summary>
        private readonly ILogger<KafkaManagerAdapter>? _logger;

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private readonly IConfiguration? _configuration;

        /// <summary>
        /// Scope Factory.
        /// </summary>
        private readonly IServiceScopeFactory? _scopeFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerTransaction"/> class.
        /// </summary>
        /// <param name="queueManagerAdapter">QueueManagerAdapter.</param>
        /// <param name="configuration">Configuration manager.</param>
        /// <param name="logger">Logger manager.</param>
        /// <param name="scopeFactory">Scope Factory.</param>
        public ConsumerTransaction(IConfiguration configuration, IQueueManagerAdapter queueManagerAdapter, ILogger<KafkaManagerAdapter> logger,
        IServiceScopeFactory scopeFactory)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._queueManagerAdapter = queueManagerAdapter;
            this._scopeFactory = scopeFactory;
            this._queueManagerAdapter.OnMessageReceive += QueueManagerAdapter_OnMessageReceive;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods And Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string? topic = this._configuration?["Kafka:Topic"];

            if (!string.IsNullOrEmpty(topic))
            {
                try
                {
                    this._queueManagerAdapter?.Subscribe(topic, stoppingToken);
                    _logger?.LogInformation("Kafka subscription iniciada.");

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        await Task.Delay(1000, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error al iniciar la suscripción de Kafka.");
                }
            }
            else
            {
                _logger?.LogWarning("El nombre del tópico de Kafka no está definido en la configuración.");
            }


        }


        /// <summary>
        /// Metodo usado para recibir los datos a traves del socket.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Objeto recibido.</param>
        private void QueueManagerAdapter_OnMessageReceive(object? sender, InputMessageEventHandler e)
        {
            try
            {
                using var scope = this._scopeFactory?.CreateScope();
                var transactionBussiness = scope?.ServiceProvider.GetRequiredService<ITransactionBussiness>();

                if (string.IsNullOrEmpty(e.Message))
                    return;

                this._logger?.LogInformation($"Received 123 inventory update: {e.Message}");
                TransactionResponse? transactionResponse = JsonSerializer.Deserialize<TransactionResponse>(e.Message);

                if (transactionResponse != null && !string.IsNullOrEmpty(transactionResponse.TransactionExternalId))
                    transactionBussiness?.Update(transactionResponse.TransactionExternalId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "QueueManagerAdapter_OnMessageReceive.");
            }

        }

        #endregion
    }
}
