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
    public class TransactionRepositoryService : IRepository<Transaction>
    {
        private IOrderStoreContext _context;
        private IUserContext _userContext;

        public TransactionRepositoryService(IOrderStoreContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task AddAsync(Transaction entity)
        {
            var updateTransaction = Mapper.Map<Transaction, UpdateTransactionRequest>(entity);
            var dbTransaction = Mapper.Map<UpdateTransactionRequest, DbTransaction>(updateTransaction);
            _context.Transactions.Add(dbTransaction);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            var dbTransactions = await _context.Transactions.OrderBy(e => e.TransactionId).ToArrayAsync();
            return dbTransactions.Select(p => Mapper.Map<Transaction>(p)).ToList();
        }

        public async Task<Transaction> GetAsync(int id)
        {
            var dbTransactions = await _context.Transactions.Where(e => e.TransactionId == id).ToArrayAsync();

            if (dbTransactions.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbTransaction, Transaction>(dbTransactions[0]);
        }

        public async Task RemoveAsync(Transaction entity)
        {
            var dbTransactions = await _context.Transactions.Where(p => p.TransactionId == entity.TransactionId).ToArrayAsync();
            if (dbTransactions.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var dbPosition = dbTransactions[0];
            _context.Transactions.Remove(dbPosition);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction entity)
        {
            var dbTransactions = await _context.Transactions.Where(p => p.TransactionId == entity.TransactionId).ToArrayAsync();
            if (dbTransactions.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var updateTransaction = Mapper.Map<Transaction, UpdateTransactionRequest>(entity);
            var dbTransaction = dbTransactions[0];
            Mapper.Map(updateTransaction, dbTransaction);

            await _context.SaveChangesAsync();
        }
    }
}
