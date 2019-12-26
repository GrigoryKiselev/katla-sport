using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.OrderStore;
using DbEmployee = KatlaSport.DataAccess.OrderStore.StoreOrder;

namespace KatlaSport.Services.OrderManagement
{
    public class OrderRepositoryService : IRepository<Order>
    {
        private IOrderStoreContext _context;
        private IUserContext _userContext;

        public OrderRepositoryService(IOrderStoreContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task<Order> GetAsync(int id)
        {
            var dbOrder = await _context.Orders.Where(e => e.Id == id).ToArrayAsync();

            if (dbOrder.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbEmployee, Order>(dbOrder[0]);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var dbEmployees = await _context.Orders.ToArrayAsync();
            return dbEmployees.Select(h => Mapper.Map<Order>(h)).ToList();
        }

        public async Task RemoveAsync(Order entity)
        {
            var dbEmployees = await _context.Orders.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbEmployees.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var dbEmployee = dbEmployees[0];
            _context.Orders.Remove(dbEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Order entity)
        {
            var dbEmployee = Mapper.Map<Order, DbEmployee>(entity);

            _context.Orders.Add(dbEmployee);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            var dbEmployees = await _context.Orders.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbEmployees.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var updateEmployee = Mapper.Map<Order, UpdateOrderRequest>(entity);
            var dbEmployee = dbEmployees[0];
            Mapper.Map(updateEmployee, dbEmployee);

            await _context.SaveChangesAsync();
        }
    }
}
