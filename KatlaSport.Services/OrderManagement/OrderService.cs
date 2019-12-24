using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.OrderStore;
using DbOrder = KatlaSport.DataAccess.OrderStore.StoreOrder;

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
            var dbOrders = await _context.Orders.Where(h => h.Code == createRequest.Code).ToArrayAsync();
            if (dbOrders.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            var dbOrder = Mapper.Map<UpdateOrderRequest, DbOrder>(createRequest);
            _context.Orders.Add(dbOrder);

            await _context.SaveChangesAsync();

            return Mapper.Map<Order>(dbOrder);
        }

        /// <inheritdoc/>
        public async Task<Order> UpdateOrderAsync(int orderId, UpdateOrderRequest updateRequest)
        {
            var dbOrders = await _context.Orders.Where(p => p.Code == updateRequest.Code && p.Id != orderId).ToArrayAsync();
            if (dbOrders.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            dbOrders = await _context.Orders.Where(p => p.Id == orderId).ToArrayAsync();
            if (dbOrders.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbOrder = dbOrders[0];

            Mapper.Map(updateRequest, dbOrder);

            await _context.SaveChangesAsync();

            return Mapper.Map<Order>(dbOrder);
        }

        /// <inheritdoc/>
        public async Task DeleteOrderAsync(int orderId)
        {
            var dbOrders = await _context.Orders.Where(p => p.Id == orderId).ToArrayAsync();
            if (dbOrders.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbOrder = dbOrders[0];
            if (dbOrder.IsDeleted == false)
            {
                throw new RequestedResourceHasConflictException();
            }

            _context.Orders.Remove(dbOrder);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task SetStatusAsync(int orderId, bool deletedStatus)
        {
            var dbOrders = await _context.Orders.Where(h => h.Id == orderId).ToArrayAsync();

            if (dbOrders.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbOrder = dbOrders[0];
            if (dbOrder.IsDeleted != deletedStatus)
            {
                dbOrder.IsDeleted = deletedStatus;
                dbOrder.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
