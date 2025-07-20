//-----------------------------------------------------------------------
// <copyright file="Transaction" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 18:17:53</date>
// <summary>Código fuente interfaz Transaction.</summary>
//-----------------------------------------------------------------------
namespace ArkanoData.Entities
{
    /// <summary>
    /// Transaction.
    /// </summary>
	public class Transaction
    {
        #region Attributes

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Transaction External Id.
        /// </summary>
        public string? TransactionExternalId { get; set; }

        /// <summary>
        /// Gets or sets Source Account Id.
        /// </summary>
        public string? SourceAccountId { get; set; }

        /// <summary>
        /// Gets or sets Target Account Id.
        /// </summary>
        public string? TargetAccountId { get; set; }

        /// <summary>
        /// Gets or sets Status.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets Tranfer Type Id.
        /// </summary>
        public int TranferTypeId { get; set; }

        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets Created At.
        /// </summary>
        public DateTime? CreatedAt { get; set; }


        #endregion

        #region Methods And Functions

        #endregion
    }
}
