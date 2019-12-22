namespace KatlaSport.Services.OrderManagement
{
    /// <summary>
    /// Represents a request for creating and updating a hive.
    /// </summary>
    public class UpdateTransactionRequest
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
        /// Gets or sets transaction id.
        /// </summary>
        public int TotalSumm { get; set; }
    }
}