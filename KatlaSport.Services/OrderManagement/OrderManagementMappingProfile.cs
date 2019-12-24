using System;
using AutoMapper;
using DataAccessOrder = KatlaSport.DataAccess.OrderStore.StoreOrder;
using DataAccessTransaction = KatlaSport.DataAccess.OrderStore.StoreTransaction;

namespace KatlaSport.Services.OrderManagement
{
    public sealed class OrderManagementMappingProfile : Profile
    {
        public OrderManagementMappingProfile()
        {
            CreateMap<DataAccessOrder, OrderListItem>();
            CreateMap<DataAccessOrder, Order>();
            CreateMap<DataAccessTransaction, TransactionListItem>();
            CreateMap<DataAccessTransaction, Transaction>();
            CreateMap<UpdateOrderRequest, DataAccessOrder>()
            .ForMember(r => r.LastUpdated, opt => opt.MapFrom(p => DateTime.UtcNow));
            CreateMap<UpdateTransactionRequest, DataAccessTransaction>()
            .ForMember(r => r.PaymentDate, opt => opt.MapFrom(p => DateTime.UtcNow));
        }
    }
}
