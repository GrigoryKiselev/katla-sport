namespace KatlaSport.DataAccess.OrderStore
{
    /// <summary>
    /// Represents a context for order store domain.
    /// </summary>
    public interface IOrderStoreContext : IAsyncEntityStorage
    {
        /// <summary>
        /// Gets a set of <see cref="OrderItem"/> entities.
        /// </summary>
        IEntitySet<StoreOrder> Orders { get; }

        /// <summary>
        /// Gets a set of <see cref="OrderItem"/> entities.
        /// </summary>
        IEntitySet<StoreTransaction> Transactions { get; }
    }
}
