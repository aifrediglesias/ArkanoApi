//-----------------------------------------------------------------------
// <copyright file="TransactionResponse" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 19:52:13</date>
// <summary>Código fuente interfaz TransactionResponse.</summary>
//-----------------------------------------------------------------------
namespace ArkanoBussiness.Dtos
{
    /// <summary>
    /// TransactionResponse.
    /// </summary>
	public class TransactionResponse
    {
        #region Attributes

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionResponse"/> class.
        /// </summary>
        public TransactionResponse()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Transaction External Id.
        /// </summary>
        public string? TransactionExternalId { get; set; }

        /// <summary>
        /// Gets or sets Created At.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        #endregion

        #region Methods And Functions

        #endregion
    }
}
