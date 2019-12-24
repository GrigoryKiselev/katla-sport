namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a hive.
    /// </summary>
    public class OrderListItem
    {
        /// <summary>
        /// Gets or sets order identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a store hive code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets transaction id.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets transaction id.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a hive is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
