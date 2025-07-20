//-----------------------------------------------------------------------
// <copyright file="IQueueManagerAdapter" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>19/07/2025 11:11:41</date>
// <summary>Código fuente clase IQueueManagerAdapter.</summary>
//-----------------------------------------------------------------------
using System.Transactions;

namespace ArkanoAdapter
{
    /// <summary>
    /// IQueueManagerAdapter.
    /// </summary>
	public interface IQueueManagerAdapter
    {
        /// <summary>
        /// Receive data event.
        /// </summary>
        event EventHandler<InputMessageEventHandler> OnMessageReceive;

        /// <summary>
        /// Method used to dispose all memory.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Method used to Send Message To Queue.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="message">Message to be send.</param>
        /// <returns></returns>
        Task ProduceAsync(string topic, string message);

        /// <summary>
        /// Method used to Receive Message From Queue.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="stoppingToken">Cancellation token.</param>
        void Subscribe(string topic, CancellationToken stoppingToken);
    }
}
