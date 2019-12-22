using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.OrderStore
{
    internal sealed class StoreTransactionConfiguration : EntityTypeConfiguration<StoreTransaction>
    {
        public StoreTransactionConfiguration()
        {
            ToTable("transactions");
            HasKey(i => i.TransactionId);
            HasRequired(i => i.StoreOrder).WithMany(i => i.Transactions).HasForeignKey(i => i.StoreOrderId);
            Property(i => i.TransactionId).HasColumnName("transaction_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.PaymentDate).HasColumnName("payment_date").IsRequired();
            Property(i => i.TotalSumm).HasColumnName("total_summ").IsRequired();
        }
    }
}