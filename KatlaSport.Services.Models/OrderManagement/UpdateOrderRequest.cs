namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a request for creating and updating a hive.
    /// </summary>
    public class UpdateOrderRequest
    {
        /// <summary>
        /// Gets or sets a store Order name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a store hive code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets a store hive address.
        /// </summary>
        public string Address { get; set; }
    }
}