//-----------------------------------------------------------------------
// <copyright file="TransactionRepository" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifre</author>
// <date>18/07/2025 18:55:45</date>
// <summary>Código fuente interfaz TransactionRepository.</summary>
//-----------------------------------------------------------------------
namespace ArkanoData.Repositories
{
    using ArkanoData.Entities;
    using ArkanoData.UnitOfWork;

    /// <summary>
    /// TransactionRepository.
    /// </summary>
	public class TransactionRepository : ITransactionRepository
    {
        #region Attributes

        /// <summary>
        /// Unit Of Work.
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Db Context.
        /// </summary>
        private ArkanoContext? _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        /// <param name="arkanoContext">Db Context.</param>
        public TransactionRepository(ArkanoContext arkanoContext)
        {
            this._context = arkanoContext;
            this._unitOfWork = new UnitOfWork(arkanoContext);
        }

        #endregion

        #region Properties

        #endregion

        #region Methods And Functions

        /// <summary>
        /// Method used to Insert a new transaction.
        /// </summary>
        /// <param name="transaction">Objet that containt the new transaction.</param>
        /// <returns></returns>
        public void Insert(Transaction transaction)
        {
            try
            {
                this._context?.Transactions.Add(transaction);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Method used to get transaction by Id.
        /// </summary>
        /// <param name="transactionExternalId">Transaction External Id</param>
        /// <returns></returns>
        public Transaction? Get(string transactionExternalId)
        {
            try
            {
                return this._context?.Transactions
                    .FirstOrDefault(f => f.TransactionExternalId == transactionExternalId);

            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Method used to Update the transaction.
        /// </summary>
        /// <param name="transaction">Objet that containt the new transaction.</param>
        public void Update(Transaction transaction)
        {
            try
            {
                this._context?.Transactions.Update(transaction);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        #endregion
    }
}
