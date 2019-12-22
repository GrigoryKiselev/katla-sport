using System;

namespace KatlaSport.Services.OrderManagment
{
    public class Order
    {
        /// <summary>
        /// Gets or sets order identifier.
         /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets a date of the order.
        /// </summary>
        public DateTime LastUpdated { get; set; }

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
