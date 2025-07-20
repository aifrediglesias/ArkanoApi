//-----------------------------------------------------------------------
// <copyright file="TransactionBussiness" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 19:49:55</date>
// <summary>Código fuente clase TransactionBussiness.</summary>
//-----------------------------------------------------------------------
namespace ArkanoBussiness
{
    using ArkanoBussiness.Dtos;

    /// <summary>
    /// TransactionBussiness.
    /// </summary>
	public interface ITransactionBussiness
    {
        /// <summary>
        /// Method used to Get One Transaction.
        /// </summary>
        /// <param name="transactionExternalId">Transaction External Id.</param>
        /// <returns>Object with transaction info.</returns>
        TransactionResponse? Get(string transactionExternalId);

        /// <summary>
        /// Method used to Insert a new transaction.
        /// </summary>
        /// <param name="transaction">Objet that containt the new transaction.</param>
        /// <returns>Transaction created.</returns>
        TransactionResponse? Insert(TransactionRequest transaction);

        /// <summary>
        /// Method used to Update the transaction.
        /// </summary>
        /// <param name="transactionExternalId">Transaction External Id.</param>
        void Update(string transactionExternalId);
    }
}
