using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.OrderStore;
using DbHive = KatlaSport.DataAccess.OrderStore.StoreOrder;

namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a order service.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderStoreContext _context;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class with specified <see cref="IOrderStoreContext"/> and <see cref="IUserContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="IOrderStoreContext"/>.</param>
        /// <param name="userContext">A <see cref="IUserContext"/>.</param>
        public OrderService(IOrderStoreContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException();
        }

        /// <inheritdoc/>
        public async Task<List<OrderListItem>> GetOrdersAsync()
        {
            var dbOrders = await _context.Orders.OrderBy(h => h.Id).ToArrayAsync();
            var orders = dbOrders.Select(h => Mapper.Map<OrderListItem>(h)).ToList();

            return orders;
        }

        /// <inheritdoc/>
        public async Task<Order> GetOrderAsync(int orderId)
        {
            var dbOrders = await _context.Orders.Where(h => h.Id == orderId).ToArrayAsync();
            if (dbOrders.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbOrder, Order>(dbOrders[0]);
        }

        /// <inheritdoc/>
        public async Task<Order> CreateOrderAsync(UpdateOrderRequest createRequest)
        {
            var dbHives = await _context.Orders.Where(h => h.Code == createRequest.Code).ToArrayAsync();
            if (dbHives.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            var dbHive = Mapper.Map<UpdateOrderRequest, DbHive>(createRequest);
            dbHive.CreatedBy = _userContext.UserId;
            dbHive.LastUpdatedBy = _userContext.UserId;
            _context.Hives.Add(dbHive);

            await _context.SaveChangesAsync();

            return Mapper.Map<Hive>(dbHive);
        }

        /// <inheritdoc/>
        public async Task<Order> UpdateOrderAsync(int hiveId, UpdateOrderRequest updateRequest)
        {
            var dbHives = await _context.Hives.Where(p => p.Code == updateRequest.Code && p.Id != hiveId).ToArrayAsync();
            if (dbHives.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            dbHives = await _context.Hives.Where(p => p.Id == hiveId).ToArrayAsync();
            if (dbHives.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbHive = dbHives[0];

            Mapper.Map(updateRequest, dbHive);
            dbHive.LastUpdatedBy = _userContext.UserId;

            await _context.SaveChangesAsync();

            return Mapper.Map<Hive>(dbHive);
        }

        /// <inheritdoc/>
        public async Task DeleteOrderAsync(int hiveId)
        {
            var dbHives = await _context.Hives.Where(p => p.Id == hiveId).ToArrayAsync();
            if (dbHives.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbHive = dbHives[0];
            if (dbHive.IsDeleted == false)
            {
                throw new RequestedResourceHasConflictException();
            }

            _context.Hives.Remove(dbHive);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task SetStatusAsync(int hiveId, bool deletedStatus)
        {
            var dbHives = await _context.Hives.Where(h => h.Id == hiveId).ToArrayAsync();

            if (dbHives.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbHive = dbHives[0];
            if (dbHive.IsDeleted != deletedStatus)
            {
                dbHive.IsDeleted = deletedStatus;
                dbHive.LastUpdated = DateTime.UtcNow;
                dbHive.LastUpdatedBy = _userContext.UserId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
