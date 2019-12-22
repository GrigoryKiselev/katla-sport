using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a order service.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Gets a order list.
        /// </summary>
        /// <returns>A <see cref="Task{List{OrderListItem}}"/>.</returns>
        Task<List<OrderListItem>> GetOrdersAsync();

        /// <summary>
        /// Gets a order with specified identifier.
        /// </summary>
        /// <param name="orderId">An order identifier.</param>
        /// <returns>A <see cref="Task{Order}"/>.</returns>
        Task<Order> GetOrderAsync(int orderId);

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="createRequest">A <see cref="UpdateOrderRequest"/>.</param>
        /// <returns>A <see cref="Task{Order}"/>.</returns>
        Task<Order> CreateOrderAsync(UpdateOrderRequest createRequest);

        /// <summary>
        /// Updates an existed order.
        /// </summary>
        /// <param name="orderId">A order identifier.</param>
        /// <param name="updateRequest">A <see cref="UpdateOrderRequest"/>.</param>
        /// <returns>A <see cref="Task{Order}"/>.</returns>
        Task<Order> UpdateOrderAsync(int orderId, UpdateOrderRequest updateRequest);

        /// <summary>
        /// Deletes an existed order.
        /// </summary>
        /// <param name="orderId">A order identifier.</param>
        Task DeleteOrderAsync(int orderId);

        /// <summary>
        /// Sets deleted status for a order.
        /// </summary>
        /// <param name="orderId">A order identifier.</param>
        /// <param name="deletedStatus">Status.</param>
        Task SetStatusAsync(int orderId, bool deletedStatus);
    }
}