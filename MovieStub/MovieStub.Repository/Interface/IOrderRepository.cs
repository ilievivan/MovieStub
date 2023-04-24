using MovieStub.Domain;
using MovieStub.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStub.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
