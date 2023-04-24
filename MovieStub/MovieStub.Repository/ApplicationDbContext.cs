using MovieStub.Domain.DomainModels;
using MovieStub.Domain.Identity;
using MovieStub.Domain.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MovieStub.Repository
{
    public class ApplicationDbContext : IdentityDbContext<MovieStubAppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCards { get; set; }
        public virtual DbSet<MovieInShoppingCart> MovieInShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<MovieInShoppingCart>()
                .HasKey(z => new { z.MovieId, z.ShoppingCartId });

            builder.Entity<MovieInShoppingCart>()
                .HasOne(z => z.CurrnetMovie)
                .WithMany(z => z.MovieInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<MovieInShoppingCart>()
                .HasOne(z => z.UserCart)
                .WithMany(z => z.MovieInShoppingCarts)
                .HasForeignKey(z => z.MovieId);

            builder.Entity<ShoppingCart>()
                .HasOne<MovieStubAppUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            builder.Entity<MovieInOrder>()
              .HasKey(z => new { z.MovieId, z.OrderId });

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.Movie)
                .WithMany(z => z.MovieInOrders)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.MovieInOrders)
                .HasForeignKey(z => z.MovieId);
        }
    }
}
