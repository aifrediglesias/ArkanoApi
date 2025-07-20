//-----------------------------------------------------------------------
// <copyright file="UnitOfWork" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 17:51:16</date>
// <summary>Código fuente interfaz UnitOfWork.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace ArkanoData.UnitOfWork
{
    /// <summary>
    /// UnitOfWork.
    /// </summary>
	internal class UnitOfWork : IUnitOfWork
    {
        #region Attributes

        /// <summary>
        /// Db Context.
        /// </summary>
        private readonly ArkanoContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="arkanoContext">Db Context.</param>
        public UnitOfWork(ArkanoContext arkanoContext)
        {
            this._context = arkanoContext;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets
        /// </summary>
        public int Property { get; set; }


        #endregion

        #region Methods And Functions

        /// <summary>
        /// Method used to free resources in memory.
        /// </summary>
        public void Dispose()
        {
            this._context.Dispose();
        }

        /// <summary>
        /// Managmenting the storage when the changes were made.
        /// </summary>
        public Task SaveChanges()
        {
            return Task.FromResult(this._context.SaveChanges());
        }

        /// <summary>
        /// Managmenting the storage when the changes were made asynchronously.
        /// </summary>
        public Task SaveChangesAsync()
        {
            return Task.FromResult(this._context.SaveChangesAsync());
        }

        #endregion
    }
}
