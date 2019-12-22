using System;

namespace KatlaSport.Services.OrderManagment
{
    public class Transaction
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
    }
}
