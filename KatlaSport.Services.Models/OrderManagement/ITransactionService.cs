using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a transaction service.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Gets a Transaction list.
        /// </summary>
        /// <returns>A <see cref="Task{List{TransactionListItem}}"/>.</returns>
        Task<List<TransactionListItem>> GetTransactionsAsync();

        /// <summary>
        /// Gets a transaction with specified identifier.
        /// </summary>
        /// <param name="transactionId">An transaction identifier.</param>
        /// <returns>A <see cref="Task{Transaction}"/>.</returns>
        Task<Transaction> GetTransactionAsync(int transactionId);

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="createRequest">A <see cref="UpdateTransactionRequest"/>.</param>
        /// <returns>A <see cref="Task{Transaction}"/>.</returns>
        Task<Transaction> CreateTransactionAsync(UpdateTransactionRequest createRequest);

        Task<List<TransactionListItem>> GetTransactionsAsync(int orderId);

        /// <summary>
        /// Updates an existed transaction.
        /// </summary>
        /// <param name="transactionId">A transaction identifier.</param>
        /// <param name="updateRequest">A <see cref="UpdateTransactionRequest"/>.</param>
        /// <returns>A <see cref="Task{Transaction}"/>.</returns>
        Task<Transaction> UpdateTransactionAsync(int transactionId, UpdateTransactionRequest updateRequest);

        /// <summary>
        /// Deletes an existed transaction.
        /// </summary>
        /// <param name="transactionId">A transaction identifier.</param>
        Task DeleteTransactionAsync(int transactionId);

        /// <summary>
        /// Sets deleted status for a transaction.
        /// </summary>
        /// <param name="transactionId">A transaction identifier.</param>
        /// <param name="deletedStatus">Status.</param>
        Task SetStatusAsync(int transactionId, bool deletedStatus);
    }
}