//-----------------------------------------------------------------------
// <copyright file="TransactionController" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 20:23:52</date>
// <summary>Código fuente interfaz TransactionController.</summary>
//-----------------------------------------------------------------------
using ArkanoAdapter;
using ArkanoBussiness;
using ArkanoBussiness.Dtos;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ArkanoApi.Controllers
{
    /// <summary>
    /// Class.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        #region Attributes

        /// <summary>
        /// QueueManagerAdapter.
        /// </summary>
        private readonly IQueueManagerAdapter? _queueManagerAdapter;

        /// <summary>
        /// Attribute used to application Logger.
        /// </summary>
        private readonly ILogger<TransactionController> _logger;

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private readonly IConfiguration? _configuration;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="configuration">Configuration manager.</param>
        /// <param name="logger">Application Logger.</param>
        /// <param name="queueManagerAdapter">QueueManagerAdapter.</param>
        /// <param name="transactionBussiness">Transaction bussiness.</param>
        public TransactionController(IConfiguration configuration, ILogger<TransactionController> logger, IQueueManagerAdapter queueManagerAdapter, ITransactionBussiness transactionBussiness)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._queueManagerAdapter = queueManagerAdapter;
            this.TransactionBussiness = transactionBussiness;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Transaction bussiness.
        /// </summary>
        public ITransactionBussiness? TransactionBussiness { get; set; }

        #endregion

        #region Methods And Functions

        /// <summary>
        /// Method used to Insert new Transaction.
        /// </summary>
        /// <param name="transactionRequest">Transaction Request.</param>
        [HttpPost]
        public IActionResult Post(TransactionRequest transactionRequest)
        {
            try
            {
                string? topic = this._configuration?["Kafka:Topic"];
                TransactionResponse? transactionResponse = this.TransactionBussiness?.Insert(transactionRequest);
                if (transactionResponse != null)
                {
                    var message = JsonSerializer.Serialize(transactionResponse);
                    if (!string.IsNullOrEmpty(topic))
                        this._queueManagerAdapter?.ProduceAsync(topic, message);
                }
                return Ok();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return Problem();
            }
        }

        /// <summary>
        /// Method used to Get One Transaction.
        /// </summary>
        /// <param name="transactionExternalId">Transaction External Id.</param>
        /// <returns>Object with transaction info.</returns>
        [HttpGet]
        public IActionResult Get(string transactionExternalId)
        {
            try
            {
                TransactionResponse? transactionResponse = this.TransactionBussiness?.Get(transactionExternalId);
                return Ok(transactionResponse);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                return Problem();
            }
        }

        #endregion
    }
}
