//-----------------------------------------------------------------------
// <copyright file="ArkanoContext" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 18:01:51</date>
// <summary>Código fuente interfaz ArkanoContext.</summary>
//-----------------------------------------------------------------------
using ArkanoData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ArkanoData
{
    /// <summary>
    /// ArkanoContext.
    /// </summary>
	public class ArkanoContext : DbContext
    {
        #region Attributes

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArkanoContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public ArkanoContext(DbContextOptions<ArkanoContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Transaction Entity.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        #endregion

        #region Methods And Functions

        /// <summary>
        /// Method used to configure connection string.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(this._configuration["ConnectionStrings:ConnectionString"]);
            }*/
        }

        /// <summary>
        /// Method used to configure entities of database model.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions", "public")
                .HasKey(h => h.TransactionExternalId);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
