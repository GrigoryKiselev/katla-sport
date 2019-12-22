using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.OrderStore;
using DbTransaction = KatlaSport.DataAccess.OrderStore.StoreTransaction;

namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a hive section service.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly IOrderStoreContext _context;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class with specified <see cref="IOrderStoreContext"/> and <see cref="IUserContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="IProductStoreHiveContext"/>.</param>
        /// <param name="userContext">A <see cref="IUserContext"/>.</param>
        public TransactionService(IOrderStoreContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException();
        }

        /// <inheritdoc/>
        public async Task<List<TransactionListItem>> GetTransactionsAsync()
        {
            var dbTransactions = await _context.Transactions.OrderBy(s => s.TransactionId).ToArrayAsync();
            var transactions = dbTransactions.Select(s => Mapper.Map<TransactionListItem>(s)).ToList();
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<Transaction> GetTransactionAsync(int transactionId)
        {
            var dbHiveSections = await _context.Transactions.Where(s => s.TransactionId == transactionId).ToArrayAsync();
            if (dbHiveSections.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbTransaction, Transaction>(dbHiveSections[0]);
        }

        /// <inheritdoc/>
        public async Task<List<TransactionListItem>> GetTransactionsAsync(int orderId)
        {
            var dbTransactions = await _context.Transactions.Where(s => s.StoreOrderId == orderId).OrderBy(s => s.TransactionId).ToArrayAsync();
            var transactions = dbTransactions.Select(s => Mapper.Map<TransactionListItem>(s)).ToList();
            return transactions;
        }

        /// <inheritdoc/>
        public async Task<Transaction> CreateTransactionAsync(UpdateTransactionRequest createRequest)
        {
            var dbTransactions = await _context.Transactions.Where(h => h.BankAccountId == createRequest.BankAccountId).ToArrayAsync();
            if (dbTransactions.Length > 0)
            {
                throw new RequestedResourceHasConflictException("bankAccountId");
            }

            var dbTransaction = Mapper.Map<UpdateTransactionRequest, DbTransaction>(createRequest);
            _context.Transactions.Add(dbTransaction);

            await _context.SaveChangesAsync();

            return Mapper.Map<Transaction>(dbTransaction);
        }

        /// <inheritdoc/>
        public async Task<Transaction> UpdateTransactionAsync(int transactionId, UpdateTransactionRequest updateRequest)
        {
            var dbTransactions = await _context.Transactions.Where(h => h.BankAccountId == updateRequest.BankAccountId && h.TransactionId != transactionId).ToArrayAsync();
            if (dbTransactions.Length > 0)
            {
                throw new RequestedResourceHasConflictException("BankAccountId");
            }

            dbTransactions = await _context.Transactions.Where(h => h.TransactionId == transactionId).ToArrayAsync();
            if (dbTransactions.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbTransaction = dbTransactions[0];

            Mapper.Map(updateRequest, dbTransaction);

            await _context.SaveChangesAsync();

            return Mapper.Map<Transaction>(dbTransaction);
        }

        /// <inheritdoc/>
        public async Task DeleteTransactionAsync(int transactionId)
        {
            var dbTransactions = await _context.Transactions.Where(h => h.TransactionId == transactionId).ToArrayAsync();
            if (dbTransactions.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbTransaction = dbTransactions[0];
            if (dbTransaction.IsDeleted == false)
            {
                throw new RequestedResourceHasConflictException();
            }

            _context.Transactions.Remove(dbTransaction);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task SetStatusAsync(int transactionId, bool deletedStatus)
        {
            var dbTransactions = await _context.Transactions.Where(h => transactionId == h.TransactionId).ToArrayAsync();

            if (dbTransactions.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbTransaction = dbTransactions[0];
            if (dbTransaction.IsDeleted != deletedStatus)
            {
                dbTransaction.IsDeleted = deletedStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
