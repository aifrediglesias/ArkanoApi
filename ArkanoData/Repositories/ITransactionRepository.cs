//-----------------------------------------------------------------------
// <copyright file="ITransactionRepository" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 18:30:27</date>
// <summary>Código fuente clase ITransactionRepository.</summary>
//-----------------------------------------------------------------------
using ArkanoData.Entities;

namespace ArkanoData.Repositories
{
    /// <summary>
    /// ITransactionRepositoty.
    /// </summary>
	public interface ITransactionRepository
    {
        /// <summary>
        /// Method used to Get One Transaction.
        /// </summary>
        /// <param name="transactionExternalId">Transaction External Id.</param>
        /// <returns>Object with transaction info.</returns>
        Transaction? Get(string transactionExternalId);

        /// <summary>
        /// Method used to Insert a new transaction.
        /// </summary>
        /// <param name="transaction">Objet that containt the new transaction.</param>
        void Insert(Transaction transaction);

        /// <summary>
        /// Method used to Update the transaction.
        /// </summary>
        /// <param name="transaction">Objet that containt the new transaction.</param>
        void Update(Transaction transaction);
    }
}
