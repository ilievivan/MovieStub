﻿using MovieStub.Domain;
using MovieStub.Domain.DomainModels;
using MovieStub.Repository.Interface;
using MovieStub.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStub.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        
        public List<Order> GetAllOrders()
        {
            return this._orderRepository.GetAllOrders();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }
    }
}
