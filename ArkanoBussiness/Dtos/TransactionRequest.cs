//-----------------------------------------------------------------------
// <copyright file="TransactionDto" company="Chubb">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 19:51:04</date>
// <summary>Código fuente interfaz TransactionDto.</summary>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace ArkanoBussiness.Dtos
{
    /// <summary>
    /// TransactionDto.
    /// </summary>
	public class TransactionRequest
    {
        #region Attributes

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRequest"/> class.
        /// </summary>
        public TransactionRequest()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Transaction External Id.
        /// </summary>
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        #endregion

        #region Methods And Functions

        #endregion
    }
}
