using System;

namespace KatlaSport.DataAccess.OrderStore
{
    public class StoreTransaction
    {
        /// <summary>
        /// Gets or sets transaction id.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets order identifier.
        /// </summary>
        public int BankAccountId { get; set; }

        /// <summary>
        /// Gets or sets a date of the order.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Gets or sets transaction id.
        /// </summary>
        public int TotalSumm { get; set; }

        /// <summary>
        /// Gets or sets a store hive ID.
        /// </summary>
        public int StoreOrderId { get; set; }

        /// <summary>
        /// Gets or sets a store hive.
        /// </summary>
        public virtual StoreOrder StoreOrder { get; set; }
    }
}
