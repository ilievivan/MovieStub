using MovieStub.Domain;
using MovieStub.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStub.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
