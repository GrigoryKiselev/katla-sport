using System;
using System.Collections.Generic;

namespace KatlaSport.DataAccess.OrderStore
{
    public class StoreOrder
    {
        /// <summary>
        /// Gets or sets order identifier.
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets a collection of sections for the store hive.
        /// </summary>
        public virtual ICollection<StoreTransaction> Transactions { get; set; }
    }
}
