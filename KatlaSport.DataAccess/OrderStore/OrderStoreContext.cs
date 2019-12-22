namespace KatlaSport.DataAccess.OrderStore
{
    internal class OrderStoreContext : DomainContextBase<ApplicationDbContext>, IOrderStoreContext
    {
        public OrderStoreContext(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEntitySet<StoreOrder> Orders => GetDbSet<StoreOrder>();

        public IEntitySet<StoreTransaction> Transactions => GetDbSet<StoreTransaction>();
    }
}
