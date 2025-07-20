//-----------------------------------------------------------------------
// <copyright file="InputMessageEventHandler" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>19/07/2025 12:14:40</date>
// <summary>Código fuente interfaz InputMessageEventHandler.</summary>
//-----------------------------------------------------------------------
namespace ArkanoAdapter
{
    /// <summary>
    /// InputMessageEventHandler.
    /// </summary>
	public class InputMessageEventHandler : EventArgs
    {
        #region Attributes

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InputMessageEventHandler"/> class.
        /// </summary>
        public InputMessageEventHandler()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Message Received.
        /// </summary>
        public string? Message { get; set; }

        #endregion

        #region Methods And Functions

        #endregion
    }
}
