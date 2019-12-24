namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a request for creating and updating a hive.
    /// </summary>
    public class UpdateOrderRequest
    {
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
    }
}