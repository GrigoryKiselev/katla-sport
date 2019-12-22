using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.OrderStore
{
    internal sealed class StoreOrderConfiguration : EntityTypeConfiguration<StoreOrder>
    {
        public StoreOrderConfiguration()
        {
            ToTable("orders");
            HasKey(i => i.Id);
            Property(i => i.Id).HasColumnName("order_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.TransactionId).HasColumnName("transaction_id").IsRequired();
            Property(i => i.Price).HasColumnName("order_price").IsRequired();
            Property(i => i.LastUpdated).HasColumnName("updated").IsRequired();
            Property(i => i.Code).HasColumnName("order_code").IsRequired();
        }
    }
}
