using MovieStub.Domain.Identity;
using MovieStub.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieStub.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MovieStubAppUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MovieStubAppUser>();
        }
        public IEnumerable<MovieStubAppUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public MovieStubAppUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.MovieInShoppingCarts")
               .Include("UserCart.MovieInShoppingCarts.CurrnetMovie")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(MovieStubAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MovieStubAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(MovieStubAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
