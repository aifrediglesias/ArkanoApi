//-----------------------------------------------------------------------
// <copyright file="TransactionBussiness" company="Arkano">
//     All rights reserved.
// </copyright>
// <author>aifred</author>
// <date>18/07/2025 19:55:37</date>
// <summary>Código fuente interfaz TransactionBussiness.</summary>
//-----------------------------------------------------------------------
using ArkanoBussiness.Dtos;
using ArkanoData;
using ArkanoData.Entities;
using ArkanoData.Repositories;
using AutoMapper;

namespace ArkanoBussiness
{
    /// <summary>
    /// TransactionBussiness.
    /// </summary>
	public class TransactionBussiness : ITransactionBussiness
    {
        #region Attributes

        /// <summary>
        /// Db Context.
        /// </summary>
        private ArkanoContext? _context;

        /// <summary>
        /// Transaction Repository.
        /// </summary>
        private TransactionRepository? _transactionRepository;

        /// <summary>
        /// Mapper.
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBussiness"/> class.
        /// </summary>
        /// <param name="arkanoContext">Db Context.</param>
        /// <param name="mapper">Mapper.</param>
        public TransactionBussiness(ArkanoContext arkanoContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = arkanoContext;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Method used to get Transaction Repository.
        /// </summary>
        private ITransactionRepository? TransactionRepository
        {
            get
            {
                if (this._context != null)
                    this._transactionRepository = new TransactionRepository(this._context);

                return this._transactionRepository;
            }
        }

        #endregion

        #region Methods And Functions

        /// <summary>
        /// Method used to Get One Transaction.
        /// </summary>
        /// <param name="transactionExternalId">Transaction External Id.</param>
        /// <returns>Object with transaction info.</returns>
        public TransactionResponse? Get(string transactionExternalId)
        {
            Transaction? transaction = this.TransactionRepository?.Get(transactionExternalId);
            return this._mapper.Map<TransactionResponse>(transaction);
        }

        /// <summary>
        /// Method used to Insert a new transaction.
        /// </summary>
        /// <param name="transaction">Objet that containt the new transaction.</param>
        /// <returns>Transaction created.</returns>
        public TransactionResponse? Insert(TransactionRequest transactionRequest)
        {
            TransactionResponse? transactionResponse = null;
            Transaction transaction = this._mapper.Map<Transaction>(transactionRequest);
            if (transaction != null)
            {
                transaction.TransactionExternalId = Guid.NewGuid().ToString();
                transaction.Status = "Pending";
                transaction.CreatedAt = DateTime.UtcNow;
                this.TransactionRepository?.Insert(transaction);
                transactionResponse = this._mapper.Map<TransactionResponse>(transaction);
            }
            return transactionResponse;
        }

        /// <summary>
        /// Method used to Update the transaction.
        /// </summary>
        /// <param name="transactionExternalId">Transaction External Id.</param>
        public void Update(string transactionExternalId)
        {
            Transaction? transaction = this._context?.Transactions
                    .FirstOrDefault(f => f.TransactionExternalId == transactionExternalId);

            decimal? dailyAccumulated = this._context?.Transactions
                .Where(w => w.CreatedAt != null && w.CreatedAt.Value.Date == DateTime.UtcNow.Date)
                .Sum(s => s.Value);

            if (transaction != null && dailyAccumulated != null)
            {
                transaction.Status = (transaction.Value > 2000) || dailyAccumulated.Value > 20000 ? "Rejected" : "Approved";
                this.TransactionRepository?.Update(transaction);
            }

        }

        #endregion
    }
}
