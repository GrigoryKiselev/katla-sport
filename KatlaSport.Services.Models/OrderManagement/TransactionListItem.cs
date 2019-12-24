using System;

namespace KatlaSport.Services.OrderManagement
{
    public class TransactionListItem
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
        /// Gets or sets a value indicating whether a hive is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets transaction id.
        /// </summary>
        public int StoreOrderId { get; set; }
    }
}
