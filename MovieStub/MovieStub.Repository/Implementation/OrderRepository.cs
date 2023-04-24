using Microsoft.EntityFrameworkCore;
using MovieStub.Domain;
using MovieStub.Domain.DomainModels;
using MovieStub.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStub.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.MovieInOrders)
                .Include("MovieInOrders.Movie")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.MovieInOrders)
                .Include("MovieInOrders.Movie")
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
